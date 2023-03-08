using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AirRaidRedSea
{
    [XmlRoot("Levels")]
    public class GameLevelsXml
    {
        [XmlElement("Level")]
        public List<GameLevelXml> Levels { get; set; }

        public static GameLevelsXml Load(Stream fileStream)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameLevelsXml));
            GameLevelsXml gameLevelsXml = xmlSerializer.Deserialize(fileStream) as GameLevelsXml;
            return gameLevelsXml;
        }
    }

    [XmlRoot("Level")]
    public class GameLevelXml
    {
        [XmlAttribute("scene")]
        public string Scene { get; set; }
        [XmlAttribute("radio_music")]
        public string RadioMusic { get; set; }
        [XmlAttribute("ambient_music")]
        public string AmbientMusic { get; set; }
        [XmlAttribute("ambient_battle")]
        public string AmbientBattle { get; set; }

        [XmlElement]
        public int AircraftNumber { get; set; }
        [XmlElement("AircraftFighter")]
        public int AircraftFighterNumber { get; set; }
        [XmlElement("AircraftBomber")]
        public int AircraftBomberNumber { get; set; }
        [XmlElement("AircraftTorpedo")]
        public int AircraftTorpedoNumber { get; set; }
        [XmlElement("AircraftAssult")]
        public int AircraftAssultNumber { get; set; }
        [XmlElement("AircraftScout")]
        public int AircraftScoutNumber { get; set; }
    }
}
