using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Task3_Notes
{
    public static class Menu
    {
        public static void Start(string storagePath)
        {
            Console.WriteLine("Loading notes from storage...");
            var allNotes = LoadStorageNotes(storagePath);
            Console.WriteLine("Storage is loaded.");

            while (true)
            {
                Console.WriteLine("\n\n\n1 - search note, 2 - view note, 3 - create note, 4 - delete note, 5 - exit");
                var userChoice = AcceptIntDataFromUserInRange("\nChoose menu item, please:", 1, 5);

                switch (userChoice)
                {
                    case 1:
                        {
                            SearchNote(allNotes);
                        }; break;
                    case 2:
                        {
                            ViewNote(allNotes);
                        }; break;
                    case 3:
                        {
                            CreateNote(storagePath, allNotes);
                        }; break;
                    case 4:
                        {
                            DeleteNote(storagePath, allNotes);
                        }; break;
                    case 5:
                        {
                            Console.WriteLine("\n\nYou quit the notes program.\n\n\n");
                            return;
                        };
                }
            }
        }

        public static void SearchNote(List<Note> allNotes)
        {
            //no notes to search
            if (allNotes == null || allNotes.Count == 0)
            {
                Console.WriteLine("\n\nThere are no notes in the storage yet.");
                return;
            }
            
            //search notes
            Console.WriteLine("\n\nEnter filter string, please:");
            var filter = Console.ReadLine();

            if (filter.Trim() == "")
            {
                //print all notes
                Console.WriteLine($"\n\n{"", 30}ALL NOTES\n");
                PrintNotes(allNotes);
            }
            else
            {
                //search notes according to the filter string
                var filteredNotes = allNotes.Where(n => n.Text.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                n.Title.Contains(filter,  StringComparison.OrdinalIgnoreCase) ||
                n.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                n.CreatedOn.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)).OrderBy(n => n.Id).ToList();

                if (filteredNotes.Count == 0)
                {
                    Console.WriteLine($"\n\nThere are no notes that match the filter '{filter}'.");
                }
                else
                {
                    Console.WriteLine($"\n\n{"",21}NOTES THAT MATCH THE FILTER\n");
                    PrintNotes(filteredNotes);
                }
            }
        }

        public static void ViewNote(List<Note> allNotes)
        {
            ViewNoteInternal(allNotes, "\n\nEnter the id of the note you are looking for:");
        }

        public static void CreateNote(string storagePath, List<Note> allNotes)
        {
            Console.WriteLine("\n\nEnter your note:");
            var data = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(data))
            {
                Console.WriteLine("\n\nEmpty note is not created.");
                return;
            }

            //create note
            var lastId = 0;
            if (allNotes != null && allNotes.Count != 0)
                lastId = allNotes[^1].Id;

            var note = new Note(data, lastId);
            Console.WriteLine("\n\nThe note is created.");

            AddNote(ref allNotes, note);

            //save note to the storage
            UpdateStorage(storagePath, allNotes);
        }

        public static void DeleteNote(string storagePath, List<Note> allNotes)
        {
            var foundNote = ViewNoteInternal(allNotes, "\n\nEnter the id of the note you are going to delete:");

            if (foundNote != null)
            {
                //confirm deleting the note
                var confirm = "YES";
                Console.WriteLine($"\n\nDo you confirm deleting the note (enter exactly {confirm} to confirm)?");
                var userChoice = Console.ReadLine();

                if (userChoice != confirm)
                {
                    //cancel deleting the note
                    Console.WriteLine("\n\nDeleting the note is canceled.");
                }
                else
                {
                    //try remove the note and if it's succesfull update the storage
                    if (allNotes.Remove(foundNote))
                    {
                        Console.WriteLine("\n\nThe note was deleted.");
                        UpdateStorage(storagePath, allNotes);
                    }
                }
            }
        }


        //additional methods

        static int AcceptIntDataFromUserInRange(string message, int start, int end)
        {
            int number;

            Console.WriteLine(message);
            var data = Console.ReadLine().Trim();

            while (string.IsNullOrEmpty(data) || !int.TryParse(data, out number) || number < start || number > end)
            {
                Console.WriteLine($"\nEnter a number between {start} and {end} (including). Try again:");
                data = Console.ReadLine().Trim();
            }

            return number;
        }

        static int AcceptIntDataFromUser(string message)
        {
            int number;

            Console.WriteLine(message);
            var data = Console.ReadLine().Trim();

            while (string.IsNullOrEmpty(data) || !int.TryParse(data, out number) || number <= 0)
            {
                Console.WriteLine($"\nYou need to enter a number more than 0. Try again:");
                data = Console.ReadLine().Trim();
            }

            return number;
        }

        static List<Note> LoadStorageNotes(string storagePath)
        {
            try
            {
                var json = File.ReadAllText(storagePath);
                return JsonConvert.DeserializeObject<List<Note>>(json) ?? new List<Note>();     //empty list if file exists but is empty and a list with elements if file exists and is not empty
            }
            catch {}

            return new List<Note>();                                                            //also empty list if file was not created
        }

        static void UpdateStorage(string storagePath, List<Note> allNotes)
        {
            if (allNotes != null)
            {
                var json = JsonConvert.SerializeObject(allNotes, Formatting.Indented);
                File.WriteAllText(storagePath, json);
                Console.WriteLine("\nThe storage is updated.");
            }
        }

        static void AddNote(ref List<Note> allNotes, Note note)     //add a note to the list
        {
            if (allNotes == null)
            {
                allNotes = new List<Note>();
            }

            allNotes.Add(note);
        }

        static void PrintNotes(List<Note> notes)
        {
            foreach (var note in notes)
                Console.WriteLine(note);
        }

        static Note ViewNoteInternal(List<Note> allNotes, string message)
        {
            var searchId = AcceptIntDataFromUser(message);
            var foundNote = allNotes?.Where(n => n.Id.Equals(searchId));

            if (foundNote?.FirstOrDefault() == null)
            {
                Console.WriteLine($"\n\nThere is no note with id {searchId}.");
            }
            else
            {
                Console.WriteLine($"\n\n{"",30}FOUND NOTE\n");
                PrintNotes(new List<Note>(foundNote));
                Console.WriteLine($"\n{foundNote.FirstOrDefault().Text}");
            }

            return foundNote?.FirstOrDefault();
        }
    }
}
