using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Player
    {
        private List<bool> _VisibleTiles;
        private List<Unit> _Units = new List<Unit>();
        private Game _Game;
        private Faction _Faction;
        private float _SpeedMultiplier = 1;
        private float _ArmourMultiplier = 1;
        private float _ViewDistanceMultiplier = 1;
        private float _LightPowerMultiplier = 1;
        private float _LightRangeMultiplier = 1;
        private float _HeavyPowerMultiplier = 1;
        private float _HeavyRangeMultiplier = 1;
        private Dictionary<UnitType, List<string>> _UnitNames;
        private Random _Random;

        public IList<Unit> Units
        {
            get
            {
                if (_Units == null)
                {
                    _Units = new List<Unit>();
                    for (int i = 0; i < Game.Units.Count; i++)
                    {
                        if (Game.Units[i].Player == this) _Units.Add(Game.Units[i]);
                    }
                }
                return _Units.AsReadOnly();
            }
        }

        public Game Game
        {
            get
            {
                return _Game;
            }

            set
            {
                _Game = value;
            }
        }

        public Faction Faction
        {
            get
            {
                return _Faction;
            }

            set
            {
                _Faction = value;
            }
        }

        public float SpeedMultiplier
        {
            get
            {
                return _SpeedMultiplier;
            }

            set
            {
                _SpeedMultiplier = value;
            }
        }

        public float ArmourMultiplier
        {
            get
            {
                return _ArmourMultiplier;
            }

            set
            {
                _ArmourMultiplier = value;
            }
        }

        public float ViewDistanceMultiplier
        {
            get
            {
                return _ViewDistanceMultiplier;
            }

            set
            {
                _ViewDistanceMultiplier = value;
            }
        }

        public float LightPowerMultiplier
        {
            get
            {
                return _LightPowerMultiplier;
            }

            set
            {
                _LightPowerMultiplier = value;
            }
        }

        public float LightRangeMultiplier
        {
            get
            {
                return _LightRangeMultiplier;
            }

            set
            {
                _LightRangeMultiplier = value;
            }
        }

        public float HeavyPowerMultiplier
        {
            get
            {
                return _HeavyPowerMultiplier;
            }

            set
            {
                _HeavyPowerMultiplier = value;
            }
        }

        public float HeavyRangeMultiplier
        {
            get
            {
                return _HeavyRangeMultiplier;
            }

            set
            {
                _HeavyRangeMultiplier = value;
            }
        }

        public Player(Game game, Faction faction)
        {
            Game = game;
            _Random = new Random();
            Faction = faction;
            _VisibleTiles = new List<bool>();
            _Units = new List<Unit>();
            Game.Changed += GameChanged;
            InitializeUnitnames();
        }

        private void GameChanged()
        {
            _VisibleTiles = null;
            _Units = null;
        }

        public bool IsTileVisible(Point position)
        {
            if (_VisibleTiles == null)
            {
                _VisibleTiles = new List<bool>();
                for (int y = 0; y < Game.Terrain.Height; y++)
                {
                    for (int x = 0; x < Game.Terrain.Width; x++)
                    {
                        bool a = false;
                        for (int i = 0; i < Units.Count; i++)
                        {
                            if (MapDisplay.PointDifference(new Point(x, y), Units[i].Position) < Units[i].Type.ViewDistance)
                            {
                                a = true;
                                break;
                            }
                        }
                        _VisibleTiles.Add(a);
                    }
                }
            }
            return _VisibleTiles[position.X + position.Y * Game.Terrain.Width];
        }

        public string GetUnitName(Unit unit)
        {
            List<string> names;
            if (unit.Type == UnitType.ShipInProgress)
            {
                names = _UnitNames[((ShipInProgress)unit).ShipType];
            }
            else
            {
                names = _UnitNames[unit.Type];
            }

            foreach (string name in names)
            {
                if (Game.Units.ToList().Find(testUnit => testUnit.Name == name) == null)
                {
                    return name;
                }
            }
            return names[_Random.Next(0, names.Count)];
        }

        public void InitializeUnitnames()
        {
            List<string> submarines = new List<string>();
            List<string> battleships = new List<string>();
            List<string> battlecruisers = new List<string>();
            List<string> heavyCruisers = new List<string>();
            List<string> lightCruisers = new List<string>();
            List<string> destroyers = new List<string>();
            List<string> torpedoBoats = new List<string>();
            List<string> minesweepers = new List<string>();
            List<string> wrecks = new List<string>();
            List<string> ports = new List<string>();
            List<string> factories = new List<string>();
            List<string> lightCargoes = new List<string>();
            List<string> mediumCargoes = new List<string>();

            switch (_Faction)
            {
                #region Germany
                case Faction.Germany:

                    for (int i = 1; i < 5000; i++)
                    {
                        submarines.Add("U" + i.ToString());
                    }

                    battleships.AddRange(new string[]
                    {
                        "Bismarck",
                        "Tirpitz",
                        "Scharnhorst",
                        "Gneisenau",
                        "Hannover",
                        "Schleswig Holstein",
                        "Schlesien"
                    });

                    battlecruisers.AddRange(new string[]
                    {
                        "Deutschland",
                        "Admiral Scheer",
                        "Admiral Graf Spee"
                    });

                    heavyCruisers.AddRange(new string[]
                    {
                        "Admiral Hipper",
                        "Blücher",
                        "Prinz Eugen",
                        "Seydlitz",
                        "Lützow"
                    });

                    lightCruisers.AddRange(new string[]
                    {
                        "Leipzig",
                        "Nürnberg",
                        "Köln",
                        "Emden",
                        "Karlsruhe",
                        "Königsberg"
                    });

                    destroyers.AddRange(new string[]
                    {
                        "Leberecht Maass",
                        "Max Schultz",
                        "Karl Galster",
                        "Wilhelm Heidkamp",
                        "Anton Schmitt",
                        "Erich Giese",
                        "Hans Lody",
                        "Paul Jakobi",
                        "Bruno Heinemann",
                        "Diether von Roeder",
                        "Friedrich Ihn",
                        "Friedrich Eckoldt"
                    });
                    for (int i = 23; i <= 45; i++)
                    {
                        destroyers.Add("Z" + i.ToString());
                    }

                    torpedoBoats.AddRange(new string[]
                    {
                        "Möwe",
                        "Falke",
                        "Greif",
                        "Kondor",
                        "Albatros",
                        "Seeadler",
                        "Wolf",
                        "Iltis",
                        "Jaguar",
                        "Leopard",
                        "Luchs",
                        "Tiger"
                    });
                    for (int i = 1; i <= 36; i++)
                    {
                        torpedoBoats.Add("T" + i.ToString());
                    }

                    for (int i = 1; i <= 214; i++)
                    {
                        minesweepers.Add("M" + i.ToString());
                    }

                    wrecks.Add("Wreck");

                    ports.AddRange(new string[]
                    {
                        "Kiel",
                        "Heligoland",
                        "Danzig",
                        "Hela",
                        "Brevik",
                        "Bergen",
                        "Horten"
                    });

                    factories.AddRange(new string[]
                    {
                        "Werk",
                        "Fabrik"
                    });

                    lightCargoes.AddRange(new string[]
                    {
                        "SS Corona",
                        "SS Erna",
                        "Dorothea Weber",
                        "MV Pelikan",
                        "SS Deneb",
                        "SS Selnes",
                        "SS Katong",
                        "SS Elbe",
                        "SS Lasknes",
                        "SS Dollart",
                        "SS Minna",
                        "SS Lowland",
                        "SS I P Suhr",
                        "SS Katong"
                    });

                    mediumCargoes.AddRange(new string[]
                    {
                        "SS Haga",
                        "SS Kolno",
                        "SS Günther Russ",
                        "SS Narva",
                        "SS Ganter",
                        "MS Totila",
                        "MS Sinfra",
                        "MV Clio",
                        "SS Lichtenfels",
                        "SS Friedrich Bischoff",
                        "SS Masuren"
                    });
                    break;
                #endregion

                #region England
                case Faction.England:

                    submarines.AddRange(new string[]
                    {
                        "HMS Cachalot",
                        "HMS Clyde",
                        "HMS Vox",
                        "HMS Grampus",
                        "HMS Talent",
                        "HMS Oxley",
                        "HMS Otus",
                        "HMS Seraph",
                        "HMS Totem",
                        "HMS Severn",
                        "HMS Porpoise",
                        "HMS Ultor",
                        "HMS Shark",
                        "HMS Regent",
                        "HMS Turpin",
                        "HMS Pandora",
                        "HMS Turbulous",
                        "HMS Rover",
                        "HMS Swordfish",
                        "HMS Rainbow"
                    });

                    battleships.AddRange(new string[]
                    {
                        "HMS Valiant",
                        "HMS King George V",
                        "HMS Howe",
                        "HMS Iron Duke",
                        "HMS Royal Oak",
                        "HMS Warspite",
                        "HMS Nelson",
                        "HMS Queen Elizabeth",
                        "HMS Resolution",
                        "HMS Anson",
                        "HMS Centurion",
                        "HMS Malaya",
                        "HMS Barham",
                        "HMS Rodney",
                        "HMS Duke of York",
                        "HMS Prince of Wales",
                        "HMS Ramillies",
                        "HMS Royal Sovereign"
                    });

                    battlecruisers.AddRange(new string[]
                    {
                        "HMS Hood",
                        "HMS Renown",
                        "HMS Repulse"
                    });

                    heavyCruisers.AddRange(new string[]
                    {
                        "HMS Vindictive",
                        "HMS Dorsetshire",
                        "HMS Effingham",
                        "HMS Hawkings",
                        "HMS Frobisher",
                        "HMS Cavendish",
                        "HMS Exeter",
                        "HMS York",
                        "HMS Kent",
                        "HMS London",
                        "HMS Norfolk",
                        "HMS Canberra",
                        "HMS Sussex",
                        "HMS Berwick",
                        "HMS Northumberland",
                        "HMS Devonshire",
                        "HMS Australia",
                        "HMS Kent",

                    });

                    lightCruisers.AddRange(new string[]
                    {
                        "HMS Belfast",
                        "HMS Caroline",
                        "HMS Calypso",
                        "HMS Liverpool",
                        "HMS Durban",
                        "HMS Centaur",
                        "HMS Fiji",
                        "HMS Nigeria",
                        "HMS Jamaica",
                        "HMS Newcastle",
                        "HMS Edinbourgh",
                        "HMS Sheffield",
                        "HMS Southampton",
                        "HMS Cambrian",
                        "HMS Concord",
                        "HMS Gambia",
                        "HMS Bermuda",
                        "HMS Birmingham",
                        "HMS Coventry",
                        "HMS Colombo",
                        "HMS Ceres",
                        "HMS Glasgow",
                        "HMS Trinidad",
                        "HMS Manchester",
                        "HMS Cardiff",
                        "HMS Curlew",
                        "HMS Gloucester",
                        "HMS Delhi",
                        "HMS Dragon",
                        "HMS Ceylon",
                        "HMS Uganda",
                        "HMS Newfoundland",
                        "HMS Capetown",
                        "HMS Caradoc",
                        "HMS Caledon",
                    });

                    destroyers.AddRange(new string[]
                    {
                        "HMS Bagley",
                        "HMS Eclipse",
                        "HMS Daring",
                        "HMS Croome",
                        "HMS Crusader",
                        "HMS Hasty",
                        "HMS Hardy",
                        "HMS Grove",
                        "HMS Arrow",
                        "HMS Basilisk",
                        "HMS Bath",
                        "HMS Havock",
                        "HMS Clare",
                        "HMS Boreas",
                        "HMS Blean",
                        "HMS Defender",
                        "HMS Ashanti",
                        "HMS Crispin",
                        "HMS Crescent",
                        "HMS Creole",
                        "HMS Grafton",
                        "HMS Hambledon",
                        "HMS Gallant",
                        "HMS Charity",
                        "HMS Garland",
                        "HMS Grenville",
                        "HMS Acasta",
                        "HMS Avon Vale",
                        "HMS Blanche",
                        "HMS Cameron",
                        "HMS Fearless",
                        "HMS Javelin",
                        "HMS Juso",
                        "HMS Impulsive",
                        "HMS Jersey",
                        "HMS Brazen"
                    });

                    for (int i = 1; i <= 150; i++)
                    {
                        torpedoBoats.Add("MTB" + i.ToString());
                    }

                    minesweepers.AddRange(new string[]
                    {
                        "HMS Jason",
                        "HMS Bangor",
                        "HMS Hythe",
                        "HMS Strenuous",
                        "HMS Tatoo",
                        "HMS Postillion",
                        "HMT Firefly",
                        "HMS Hydra",
                        "HMS Seagull",
                        "HMS Sphinx",
                        "HMS Peterhead",
                        "HMS Pickle",
                        "HMS Felixstowe",
                        "HMT Juniper"
                    });

                    wrecks.Add("Wreck");

                    ports.AddRange(new string[]
                    {
                        "Portsmouth",
                        "Plymouth",
                        "Scapa Flow",
                        "Southampton",
                        "Bristol",
                        "Kingston",
                        "Chatham",
                        "Devonport",
                        "Clyde"
                    });

                    factories.AddRange(new string[]
                    {
                        "Factory",
                        "Plant",
                        "Manufactory",
                        "Works"
                    });

                    lightCargoes.AddRange(new string[]
                    {
                        "SS M. Guhn",
                        "SS M. Michael",
                        "SS Mack Bryson",
                        "SS Mahlon Pitney",
                        "SS Malcolm M.Steward",
                        "SS Manasseh Cutler",
                        "SS Marcus Daly",
                        "SS Marcus H.Tracy",
                        "SS Mark Keppel",
                        "SS Mark Twain",
                        "SS Marshall Elliot",
                        "SS Marcus Whitman",
                        "SS Margaret Brent",
                        "SS Margaret Fuller",
                        "SS Mariscal Sucre",
                        "SS Mark A.Davis",
                        "SS Mark Hanna Mark",
                        "SS Maria Mitchell",
                        "SS M. E. Comerford",
                        "SS M. H. De Young",
                        "SS Maria Sanford",
                        "SS Marie M.Meloney",
                        "SS Marion McKinley",
                        "SS Mark Hopkins",
                        "SS Martha Berry",
                        "SS Martha C.Thomas",
                        "SS Martin Behrman",

                    });

                    mediumCargoes.AddRange(new string[]
                    {
                        "SS Arthur Middleton",
                        "SS Alex H. Stephens",
                        "SS Charles Carroll",
                        "SS Francis Scott Key",
                        "TS American Mariner",
                        "SS Christopher Newport",
                        "SS Carter Braxton",
                        "SS Benjamin Harrison",
                        "SS Francis L.Lee",
                        "SS Esek Hopkins",
                        "SS Alexander Macomb",
                        "SS Eleazar Wheelock",
                        "SS Bernard Carter",
                        "SS Andrew Hamilton",
                        "SS Benjamin Chew",
                        "SS Benjamin Franklin",
                        "SS Daniel Boone",
                        "SS Abraham Clark",
                        "SS Caleb Strong",
                        "SS F.A.C. Muhlenberg",
                        "SS Egbert Benson",
                        "SS Francis Parkman",
                        "SS Davy Crockett",
                        "SS David S.T.J",
                        "SS Benjamin Bourne",
                        "SS Daniel Carroll",
                        "SS Daniel Hiester",
                        "SS Benjamin Huntington",
                        "SS A.P.Hill",
                        "SS Big Foot Wallace", 
                        "SS Amelia Earhart",
                        "SS Champ Clark Champ",
                        "SS Abraham Baldwin"

                    });
                    break;
                #endregion

                #region USA
                case Faction.USA:

                    submarines.AddRange(new string[]
                    {
                        "USS Gato",
                        "USS Barb",
                        "USS Wahoo",
                        "USS Ciasco",
                        "USS Escolar",
                        "USS Barbel",
                        "USS Kete",
                        "USS Lagarto",
                        "USS Stickleback",
                        "USS Bullhead",
                        "USS Shark",
                        "USS Tang",
                        "USS Trumpetfish",
                        "USS Balao",
                        "USS Drum",
                        "USS Croaker",
                        "USS Halibut",
                        "USS Harder",
                        "USS Mingo",
                        "USS Guinea"
                    });

                    battleships.AddRange(new string[]
                    {
                        "USS Iowa",
                        "USS Texas",
                        "USS Illinois",
                        "USS Kentucky",
                        "USS Wisconsin",
                        "USS Missouri",
                        "USS New Jersey",
                        "USS Nevada",
                        "USS Arkansas",
                        "USS Pennsylvania",
                        "USS Oklahoma",
                        "USS Colorado",
                        "USS California",
                        "USS Washington",
                        "USS Maryland",
                        "USS West Virginia",
                        "USS Florida",
                        "USS Utah"
                    });

                    battlecruisers.AddRange(new string[]
                    {
                        "USS Constitution",
                        "USS Ranger",
                        "USS Constellation",
                        "USS United States",
                        "USS Alaska",
                        "USS Hawaii",
                        "USS Puerto Rico",
                        "USS Philippines",
                        "USS Samoa",
                        "USS Guam",
                        "USS Saratoga"
                    });

                    heavyCruisers.AddRange(new string[]
                    {
                        "USS Baltimore",
                        "USS Quincy",
                        "USS Canberra",
                        "USS Helena",
                        "USS Columbus",
                        "USS Macon",
                        "USS Fall River",
                        "USS Saint Paul",
                        "USS Toledo",
                        "USS Boston",
                        "USS Chicago",
                        "USS Los Angeles",
                        "USS Portland",
                        "USS Indianapolis",
                        "USS Pensacola",
                        "USS Salt Lake City",
                        "USS Chester",
                        "USS Houston",

                    });

                    lightCruisers.AddRange(new string[]
                    {
                        "USS Cleveland",
                        "USS Mobile",
                        "USS Montpelier",
                        "USS Denver",
                        "USS Columbia",
                        "USS Santa Fe",
                        "USS Amsterdam",
                        "USS Pasadena",
                        "USS Springfield",
                        "USS Topeka",
                        "USS New Haven",
                        "USS Buffalo",
                        "USS Fargo",
                        "USS Biloxi",
                        "USS Dayton",
                        "USS Wilmington",
                        "USS Providence",
                        "USS Miami",
                        "USS Astoria",
                        "USS Little Rock",
                        "USS Youngstown",
                        "USS Omaha",
                        "USS Detroit",
                        "USS Richmond",
                        "USS Concord",
                        "USS Trenton",
                        "USS Marblehead",
                        "USS Memphis",
                        "USS Raleigh",
                        "USS Brooklyn",
                        "USS Atlanta",
                        "USS Honolulu",
                        "USS Boise",
                        "USS Phoenix",
                        "USS Nashville",
                    });

                    destroyers.AddRange(new string[]
                    {
                        "USS Clemson",
                        "USS Blue",
                        "USS Patterson",
                        "USS Ralph Talbot",
                        "USS Helm",
                        "USS Mugford",
                        "USS Henley",
                        "USS Jarvis",
                        "USS Russel",
                        "USS Anderson",
                        "USS Hughes",
                        "USS Sims",
                        "USS O'Brien",
                        "USS Buck",
                        "USS Wainwright",
                        "USS Mustin",
                        "USS Morris",
                        "USS Roe",
                        "USS Farragut",
                        "USS Dewey",
                        "USS Hull",
                        "USS Dale",
                        "USS Worden",
                        "USS Porter",
                        "USS Phelps",
                        "USS Clark",
                        "USS Moffett",
                        "USS Mahan",
                        "USS Smith",
                        "USS Preston",
                        "USS Benson",
                        "USS Gleaves",
                        "USS Fletcher",
                        "USS Radford",
                        "USS Jenkins",
                        "USS Stevens"
                    });

                    for (int i = 1; i <= 20; i++)
                    {
                        torpedoBoats.Add("PT" + i.ToString());
                    }

                    minesweepers.AddRange(new string[]
                    {
                        "USS Dunlin",
                        "USS Embattle",
                        "USS Device",
                        "USS Diploma",
                        "USS Dour",
                        "USS Design",
                        "USS Harlequin",
                        "USS Garland",
                        "USS Knave",
                        "USS Lance",
                        "USS Logic",
                        "USS Lucid",
                        "USS Rebel",
                        "USS Salute"
                    });

                    wrecks.Add("Wreck");

                    ports.AddRange(new string[]
                    {
                        "Beaumont",
                        "Long Beach",
                        "Boston",
                        "Houston",
                        "New York",
                        "Baltimore",
                        "Pittsburg"
                    });

                    factories.AddRange(new string[]
                    {
                        "Factory",
                        "Plant",
                        "Manufactory",
                        "Works"
                    });

                    lightCargoes.AddRange(new string[]
                    {
                        "SS John W. Brown",
                        "SS Jeremiah O'Brien",
                        "SS John C. Calhound",
                        "SS John L. Motley",
                        "SS John Bascom",
                        "SS John Harvey",
                        "SS Joseph Wheeler",
                        "SS John M. Clayton",
                        "SS Joseph G. Cannon",
                        "SS John Straub",
                        "SS Joel R. Poinsett",
                        "SS Joseph Smith",
                        "SS Joseph V. Conn",
                        "SS Louis Tiffany",
                        "SS Juan de Fuca",
                        "SS John Cabot", 
                        "SS Joseph Henry",
                        "SS Laura Keene",
                        "SS Joe Blackburn",
                        "SS John Carver",
                        "SS John Morgan",
                        "SS Josiah Snelling",
                        "SS John Burke",
                        "SS Lewis Dyche",
                        "SS Leonidas Merritt",
                        "SS Jeremiah M. Dail",
                        "SS Lee S.Overman",
                        "SS John C.Fremont",
                        "SS Johns Hopkins",
                        "SS John Randolph",
                        "SS John A.Poor",
                        "SS John H.Hammond"
                    });

                    mediumCargoes.AddRange(new string[]
                    {
                        "SS Walter Reed",
                        "SS Star of Oregon",
                        "SS Thomas Paine",
                        "SS William Clark",
                        "SS Zebulon B.",
                        "SS Thomas Jefferson",
                        "SS Stephen A.Douglas",
                        "SS Thomas MacDonough",
                        "SS Samuel Adams",
                        "SS Virginia Dare",
                        "SS William Dawes",
                        "SS William Hooper",
                        "SS Samuel Chase",
                        "SS Zebulon Pike",
                        "SS Zachary Taylor",
                        "SS Timothy Pickering",
                        "SS William C.C.Clark",
                        "SS Sam Houston",
                        "SS Thomas Nelson", 
                        "SS William Cullen",
                        "SS William Floyd",
                        "SS St.Olaf",
                        "SS Thomas Stone",
                        "SS Stephen Hopkins"
                    });
                    break;
                #endregion

                #region Japan
                case Faction.Japan:

                    for (int i = 1; i < 300; i++)
                    {
                        submarines.Add("I" + i.ToString());
                    }

                    battleships.AddRange(new string[]
                    {
                        "Yamato",
                        "Musashi",
                        "Fuso",
                        "Ise",
                        "Yamashiro",
                        "Nagato",
                        "Mutsu"
                    });

                    battlecruisers.AddRange(new string[]
                    {
                        "Kongo",
                        "Hiei",
                        "Haruna",
                        "Kirishima",
                        "Amagi"
                    });

                    heavyCruisers.AddRange(new string[]
                    {
                        "Mogami",
                        "Takao",
                        "Kako",
                        "Furutaka",
                        "Kinugasa",
                        "Tone",
                        "Atago",
                        "Haguro",
                        "Nachi",
                        "Chōkai",
                        "Mikuma",
                        "Suzuya",
                        "Kumano",
                        "Chikuma",
                        "Aoba"
                    });

                    lightCruisers.AddRange(new string[]
                    {
                        "Yahagi",
                        "Sendai",
                        "Nagara",
                        "Kuma",
                        "Agano",
                        "Jintsu",
                        "Kiso",
                        "Isuzu",
                        "Yura",
                        "Natori",
                        "Naka",
                        "Noshiro"
                    });

                    destroyers.AddRange(new string[]
                    {
                        "Akebono",
                        "Amagiri",
                        "Asagiri",
                        "Ayanami",
                        "Fubuki",
                        "Hatsuyuki",
                        "Isonami",
                        "Miyuki",
                        "Murakumo",
                        "Oboro",
                        "Sagiri",
                        "Sazanami",
                        "Shikinami",
                        "Shinonome",
                        "Shirakumo",
                        "Shirayuki",
                        "Uranami",
                        "Ushio",
                        "Usugumo",
                        "Yūgiri"
                    });

                    torpedoBoats.AddRange(new string[]
                    {
                        "Tomozuru",
                        "Manazuru",
                        "Chidori",
                        "Hatsukari",
                        "Otori",
                        "Kiji",
                        "Kari",
                        "Sagi",
                        "Hato",
                        "Hayabusa",
                        "Hiyodori"
                    });

                    minesweepers.AddRange(new string[]
                    {
                        "Okinoshima",
                        "Hatsutaka",
                        "Shirataka",
                        "Tsugaru",
                        "Kamishima",
                        "Minoo",
                        "Yaeyama"
                    });

                    wrecks.Add("Wreck");

                    ports.AddRange(new string[]
                    {
                        "Kobe",
                        "Yokohama",
                        "Osaka",
                        "Saiti"
                    });

                    factories.AddRange(new string[]
                    {
                        "Kojo",
                        "Fakutori"
                    });

                    lightCargoes.AddRange(new string[]
                    {
                        "Kembu Maru",
                        "Kuroshio Maru",
                        "Kyokusei Maru",
                        "Tatsuta Maru",
                        "Terukuni Maru",
                        "Tofuku Maru",
                        "Toyama Maru",
                        "Tsushima Maru",
                        "Irako",
                        "Shirasaki",
                        "Nosaki",
                        "Muroto"
                    });

                    mediumCargoes.AddRange(new string[]
                    {
                        "Aratama Maru",
                        "Arisan Maru",
                        "Asama Maru",
                        "Atago Maru",
                        "Awa Maru",
                        "Awazisan Maru",
                        "Kansai Maru",
                        "Kashi Maru",
                        "Kinesaki",
                        "Hayasaki",
                        "Kitakami Maru",
                        "Arasaki"
                    });
                    break;
                #endregion

                #region Default
                default:
                    submarines.Add("Submarine");
                    battleships.Add("Battleship");
                    battlecruisers.Add("Battlecruiser");
                    heavyCruisers.Add("Heavy Cruiser");
                    lightCruisers.Add("Light Cruiser");
                    destroyers.Add("Destroyer");
                    torpedoBoats.Add("Topredo Boat");
                    minesweepers.Add("Minesweeper");
                    wrecks.Add("Wreck");
                    ports.Add("Port");
                    factories.Add("Factory");
                    lightCargoes.Add("Light Cargo Ship");
                    mediumCargoes.Add("Medium Cargo Ship");
                    break;
                    #endregion
            }

            _UnitNames = new Dictionary<UnitType, List<string>>()
                    {
                        { UnitType.Submarine, submarines },
                        { UnitType.Battleship, battleships },
                        { UnitType.Battlecruiser, battlecruisers },
                        { UnitType.HeavyCruiser, heavyCruisers },
                        { UnitType.LightCruiser, lightCruisers },
                        { UnitType.Destroyer, destroyers },
                        { UnitType.TorpedoBoat, torpedoBoats },
                        { UnitType.Minesweeper, minesweepers },
                        { UnitType.Wreck, wrecks },
                        { UnitType.Port, ports },
                        { UnitType.Factory, factories },
                        { UnitType.LightCargo, lightCargoes },
                        { UnitType.MediumCargo, mediumCargoes },
                    };
        }
    }
}