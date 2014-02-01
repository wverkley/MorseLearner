using System;
using System.Text;
using System.Collections;
using System.IO;

namespace Wxv.MorseLearner
{
	public class PakFile : IDisposable	
	{
		class PakFileEntry
		{
			public long Index;
			public string Filename;
			public int Length;
			public string FullFilename;

			public string AsString
			{
				get { return CsvUtil.Format (new object[] {Index, Filename, Length}); }
				set 
				{ 
					string[] values = CsvUtil.Extract (value);
					if (values.Length != 3)
						throw new Exception ("Invalid format");
					Index = long.Parse (values [0]);
					Filename = values [1];
					Length = int.Parse (values [2]);
					FullFilename = null;
				}
			}

			public override string ToString()
			{
				return AsString;
			}
		}

		private ArrayList pakFileInfoList = new ArrayList();

		private void LoadDirectory (string path, ref long index)
		{
			foreach (string filename in Directory.GetFiles (path))
			{
				try
				{
					FileInfo fi = new FileInfo (filename);
				
					PakFileEntry pfe = new PakFileEntry();

					pfe.Filename = filename.Substring (basePath.Length, filename.Length - basePath.Length);
					pfe.Index = index;
					pfe.Length = (int) fi.Length;
					pfe.FullFilename = filename;
					index += pfe.Length;

					pakFileInfoList.Add (pfe);
				}
				catch (Exception) {}
			}

			foreach (string directory in Directory.GetDirectories (path))
				LoadDirectory (directory, ref index);
		}

		private string PakFileEntryHeader
		{
			get { return CsvUtil.Format (pakFileInfoList); }
			set 
			{
				pakFileInfoList.Clear();
				foreach (string s in CsvUtil.Extract (value))
				{
					PakFileEntry pfe = new PakFileEntry();
					pfe.AsString = s;
					pakFileInfoList.Add (pfe);
				}
			}

		}


		private string basePath = null;

		private Stream pakStream = null;
		private long pakDataPosition = 0;

		
		private bool isPakFile;
		public bool IsPakFile
		{
			get { return isPakFile; }
		}

		private void LoadFromPakFile (Stream stream)
		{
			pakStream = stream;

			BinaryReader reader = new BinaryReader (pakStream);
			int headerSize = reader.ReadInt32();
			byte[] header = reader.ReadBytes (headerSize);
			PakFileEntryHeader = Encoding.UTF8.GetString (header);
			pakDataPosition = pakStream.Position;
		}

		private void LoadFromFileSystem(string path)
		{
			if (!path.EndsWith ("\\"))
				path += "\\";
			this.basePath = path;

			long index = 0;
			LoadDirectory (basePath, ref index);
		}

		public PakFile (string filename)
		{
            if (Directory.Exists(Path.GetFileNameWithoutExtension(filename)))
            {
                isPakFile = false;
                LoadFromFileSystem(Path.GetFileNameWithoutExtension(filename));
            }
            else
            {
                isPakFile = true;
                LoadFromPakFile(new FileStream(filename, FileMode.Open));
            }
		}


        public PakFile(byte[] data)
        {
            isPakFile = true;
            LoadFromPakFile(new MemoryStream(data));
        }


        public void Dispose()
		{
			if (pakStream != null)
			{
				pakStream.Close();
				pakStream = null;
			}
		}

		
		private Stream LoadFromPak (PakFileEntry pfe)
		{
			if (IsPakFile)
			{
				pakStream.Seek (pakDataPosition + pfe.Index, SeekOrigin.Begin);
				byte[] buffer = new byte [pfe.Length];
				pakStream.Read (buffer, 0, pfe.Length);
				return new MemoryStream (buffer);
			}
			else
				return new FileStream (pfe.FullFilename, FileMode.Open);
		}


		public Stream LoadFromPak (string filename)
		{
			filename = filename.Trim().ToLower();

			foreach (PakFileEntry pfe in pakFileInfoList)
				if (pfe.Filename.ToLower().Trim() == filename)
					return LoadFromPak (pfe);
			return null;
		}


		public void SaveToStream (Stream stream)
		{
			const int bufferSize = 1024;
			byte[] buffer = new byte[bufferSize];
			int size;

			BinaryWriter writer = new BinaryWriter (stream);
			byte[] header = Encoding.UTF8.GetBytes (PakFileEntryHeader);
			writer.Write (header.Length);
			writer.Write (header);

			foreach (PakFileEntry pfe in pakFileInfoList)
				using (Stream entryStream = LoadFromPak (pfe))
					while ((size = entryStream.Read (buffer, 0, bufferSize)) > 0)
						writer.Write (buffer, 0, size);
		}


		public void SaveToFile (string filename)
		{
			using (FileStream stream = new FileStream (filename, FileMode.Create))
				SaveToStream (stream);
		}
			
	}

}


