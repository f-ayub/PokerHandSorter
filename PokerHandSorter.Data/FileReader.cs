using System;
using System.Collections.Generic;
using PokerHandSorter.Classes;
using System.Runtime.CompilerServices;
using System.IO;

namespace PokerHandSorter.Data
{
    public class FileReader
    {
        public string FilePath { get; set; }
        public FileReader(string filePath)
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// Reads Input from the file using the file path given in arguments
        /// </summary>
        /// <returns></returns>
        public string ReadStreamOfHands()
        {
            try
            {
                string handsForDealer;

                using (StreamReader sr = new StreamReader(FilePath))
                {
                    handsForDealer = sr.ReadToEnd();
                }

                return handsForDealer;
            }
            catch(Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    throw new Exception("File could not be found on the specified path.");
                }
                else
                {
                    throw new Exception("Error occured in reading data from the file. Please ensure the format is correct and as expected.");
                }
                
            }
        }
    }
}
