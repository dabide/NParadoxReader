using System.Collections.Generic;
using Java.IO;
using Sharpen;

namespace Com.Khubla.Pdxreader.Util
{
	public class TestUtil
	{
		/// <exception cref="System.Exception"/>
		private static IList<string> GetTestFiles(string dir, IList<string> files, string
			[] extensions)
		{
			File file = new File(dir);
			string[] list = file.List();
			if (null != list)
			{
				for (int i = 0; i < list.Length; i++)
				{
					{
						string fileName = dir + list[i];
						File f2 = new File(fileName);
						if (f2.IsDirectory())
						{
							GetTestFiles(fileName + "/", files, extensions);
						}
						else
						{
							for (int j = 0; j < extensions.Length; j++)
							{
								if (fileName.ToLower().EndsWith(extensions[j].ToLower()))
								{
									files.Add(fileName);
								}
							}
						}
					}
				}
			}
			return files;
		}

		/// <exception cref="System.Exception"/>
		public static IList<string> GetTestFiles(string dir, string[] extensions)
		{
			return GetTestFiles(dir, new List<string>(), extensions);
		}
	}
}
