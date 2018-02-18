using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace MongoDbTest
{
    class Program
    {
        private static List<string> namesList;

        static void Main(string[] args)
        {
            BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator()); 
            namesList = new List<string>();
            const string PATH =
                @"C:\Users\Tyeth\Documents\REPOS\C#\facialrecognition\FaceRecProOV\bin\Debug\TrainedFaces";

            Console.WriteLine($"Searching {PATH}!");

            List<FacialCroppedMatch> list = new List<FacialCroppedMatch>();
            
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("loading ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("TrainedLabels.txt");

            LoadTrainedLabels(PATH+ "\\TrainedLabels.txt");

           
                
            

            for (int i = 0; i < namesList.Count; i++)
            {
                string file = PATH + "\\face" + (i + 1) + ".bmp";
                var imgMatch = new FacialCroppedMatch()
                {
                    Name = file,
                    Person=namesList[i]
                };
                //var imgGrey = new Image<Gray, byte>(file);

                var fileBytes = File.ReadAllBytes(file);
                MemoryStream ms = new MemoryStream(fileBytes.Length);
                ms.Write(fileBytes, 0, fileBytes.Length);
                //ms.FlushAsync();

                //imgGrey.Bitmap.Save(ms, ImageFormat.Bmp);
                //ms.Seek(0, SeekOrigin.Begin);
                imgMatch.ImageBytes = ms.ToArray();

                list.Add(imgMatch);
            }


            var dbClient = new MongoClient(); //defaults to using admin database on localhost.

            var db = dbClient.GetDatabase("faces");
                
            var collection =db.GetCollection<FacialCroppedMatch>("trustedGrey", new MongoCollectionSettings() { AssignIdOnInsert = true, ReadPreference = ReadPreference.Primary, ReadConcern = ReadConcern.Default });
            var list2 = new FacialCroppedMatch[list.Count];
            list.CopyTo(list2);
            foreach (var facialCroppedMatch in list2)
            {

                var filename = facialCroppedMatch.Name.Split('\\').Last();
                if (collection.Count(x =>
                        x.Name == facialCroppedMatch.Name && x.Person==facialCroppedMatch.Person && x.ImageBytes == facialCroppedMatch.ImageBytes) > 0)
                {
                    list.Remove(facialCroppedMatch);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Skipped duplicate (name: {facialCroppedMatch.Person} file:{filename})");
                }
                else
                {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Adding to mongodb collection (name: {facialCroppedMatch.Person} file:{filename})");
                }

            }

            if (list.Count>0)collection.InsertMany(list);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(string.Format("Test {0} images found for Tyeth of {1} in mongodb collection.\n",
                collection.Count(x => x.Person== "Tyeth"),collection.Count(x=>true)));
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("");
            Console.WriteLine("Press a key to continue");
            Console.WriteLine("");
            Console.ReadKey();
        }

        private static void LoadTrainedLabels(string file, Func<string,string> readFunc =null )
        {
            if (readFunc == null) readFunc = File.ReadAllText;
            var fs = readFunc(file);
            var elements = fs.Split('%');
             int.TryParse(elements[0], out int len);
            for (int i = 1; i < elements.Length-1; i++)
            {
                namesList.Add(elements[i]);
            }

            if (namesList.Count != len)
                throw new ArgumentOutOfRangeException($"Mismatch namesList={namesList.Count} but TrainedLabels.txt says count is {len}");
        }
    }
}
