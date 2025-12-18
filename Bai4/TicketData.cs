using System.Collections.Generic;
using System.Linq;

namespace Lab03_Bai04
{
    public class Seat
    {
        public string Id;
        public string Status;
        public Seat(string id) { Id = id; Status = "free"; }
    }

    public class Room
    {
        public string Name;
        public Dictionary<string, Seat> Seats = new Dictionary<string, Seat>();
        public Room(string name, IEnumerable<string> seatIds)
        {
            Name = name;
            foreach (var s in seatIds) Seats[s] = new Seat(s);
        }
    }

    public class Movie
    {
        public string Title;
        public int BasePrice;
        public List<string> Rooms;
        public string PosterUrl; 

        
        public Movie(string t, int p, List<string> rooms, string poster)
        {
            Title = t;
            BasePrice = p;
            Rooms = rooms;
            PosterUrl = poster;
        }
    }

    public static class TicketData
    {
        public static object DataLock = new object();
        public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();
        public static List<Movie> Movies = new List<Movie>();

        static TicketData()
        {
            Init();
        }

        public static void Init()
        {
            // Khởi tạo phòng và ghế
            var seatIds = new List<string>();
            seatIds.AddRange(Enumerable.Range(1, 5).Select(i => "A" + i));
            seatIds.AddRange(Enumerable.Range(1, 5).Select(i => "B" + i));
            seatIds.AddRange(Enumerable.Range(1, 5).Select(i => "C" + i));
            Rooms["1"] = new Room("1", seatIds);
            Rooms["2"] = new Room("2", seatIds);
            Rooms["3"] = new Room("3", seatIds);

            // Khởi tạo Phim có Link Poster
            Movies.Add(new Movie("Đào, phở và piano", 45000, new List<string> { "1", "2", "3" }, "https://upload.wikimedia.org/wikipedia/vi/2/29/Poster_Phim_%C4%90%C3%A0o%2C_ph%E1%BB%9F_v%C3%A0_piano.jpg"));
            Movies.Add(new Movie("Mai", 100000, new List<string> { "2", "3" }, "https://upload.wikimedia.org/wikipedia/vi/8/87/Mai_2024_poster.jpg"));
            Movies.Add(new Movie("Gặp lại chị bầu", 70000, new List<string> { "1" }, "https://upload.wikimedia.org/wikipedia/vi/2/26/Gap_lai_chi_bau_poster.jpg"));
            Movies.Add(new Movie("Tarot", 90000, new List<string> { "3" }, "https://upload.wikimedia.org/wikipedia/en/a/ab/Tarot_2024_poster.jpg"));
        }

        public static bool TryBook(string room, string seat)
        {
            lock (DataLock)
            {
                if (!Rooms.ContainsKey(room)) return false;
                if (!Rooms[room].Seats.ContainsKey(seat)) return false;
                if (Rooms[room].Seats[seat].Status == "booked") return false;
                Rooms[room].Seats[seat].Status = "booked";
                return true;
            }
        }
    }
}