using System;

namespace Wxv.MorseLearner
{
	public class WaveGenerator
	{
		public const short BitsPerSample = 16;
		public const short Channels = 1;

		public const int DefaultFrequency = 875;
		public const int DefaultSamplesPerSecond = 22050;
		public const int DefaultDuration = 1000;
		public const int DefaultFadeDuration = 2;
		public const double DefaultVolume = 1;
		
		private int frequency = DefaultFrequency;
		public int Frequency 
		{
			get { return frequency; }
			set { frequency = value; }
		}

		private int samplesPerSecond = DefaultSamplesPerSecond;
		public int SamplesPerSecond 
		{
			get { return samplesPerSecond; }
			set { samplesPerSecond = value; }
		}

		private int duration = DefaultDuration;
		public int Duration 
		{
			get { return duration; }
			set { duration = value; }
		}

		private int fadeDuration = DefaultFadeDuration;
		public int FadeDuration 
		{
			get { return fadeDuration; }
			set { fadeDuration = value; }
		}

		private double volume = DefaultVolume;
		public double Volume 
		{
			get { return volume; }
			set { volume = value; }
		}

		public WaveGenerator ()
		{
		}

		public WaveFormat WaveFormat 
		{
			get { return new WaveFormat (samplesPerSecond, BitsPerSample, Channels); }
		}

		public int DataLength 
		{
			get 
			{ 
				double durationSec = duration / 1000.0;
				WaveFormat format = WaveFormat;
				int dataLength = (int) (durationSec * format.AverageBytesPerSecond); 
				dataLength -= dataLength % format.BlockAlign;
				return dataLength;
			}
		}

		public byte[] GenerateData (int index, int length)
		{
			WaveFormat format = WaveFormat;
			int dataLength = DataLength;

			double durationSec = duration / 1000.0;
			double fadeDurationSec = fadeDuration / 1000.0;

			if (
				(index < 0) 
				|| (index >= dataLength) 
				|| ((index + length) > dataLength)
				|| ((index % format.BlockAlign) != 0)
				|| ((length % format.BlockAlign) != 0))
				throw new Exception ("Invalid Parameters");
      
			// Build the sine wave
			byte[] data = new byte [length];
			if (volume == 0)
				return data;

			for (int j = 0; j < length; j = j + format.BlockAlign)
			{
				int i = index + j;
				double time = ((double) i / format.AverageBytesPerSecond);
				double sinValue = Math.Sin (time * frequency * Math.PI * 2) * volume;

				if (time < fadeDurationSec)
					sinValue *= (time / fadeDurationSec);
				else if (time > (durationSec -  fadeDurationSec))
					sinValue *= (-(time - durationSec) / fadeDurationSec);

				Int32 rawValue = (Int32) (sinValue * 0x7FFF);

				data [j + 0] = (byte)(rawValue & 0xff); 
				data [j + 1] = (byte)((rawValue >> 8) & 0xff); 
			}

			return data;
		}

		public byte[] GenerateData ()
		{
			return GenerateData (0, DataLength);
		}

		public WaveBuffer Generate ()
		{
			return new WaveBuffer (WaveFormat, GenerateData());
		}

		private int index;
		private int length;
		public byte[] GenerateFirst (int length)
		{
			int dataLength = DataLength;
			length -= dataLength % WaveFormat.BlockAlign;
			index = 0;
			this.length = length;
			return GenerateNext ();
		}

		public byte[] GenerateNext ()
		{
			int dataLength = DataLength;
			if (index >= dataLength)
				return null;

			if ((index + length) > dataLength)
				length = dataLength - index;

			byte[] data = GenerateData (index, length);
			index += length;

			return data;
		}


	}
}
