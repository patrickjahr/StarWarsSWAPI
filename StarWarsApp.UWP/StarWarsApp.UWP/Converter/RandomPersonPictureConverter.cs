using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Core.Utils.Extentions;
using Model;

namespace StarWarsApp.UWP.Converter
{
    public class RandomPersonPictureConverter : IValueConverter
    {
        private static readonly List<KeyValuePair<string, BitmapImage>> _personImages = new List<KeyValuePair<string, BitmapImage>>
        {
            new KeyValuePair<string,BitmapImage>("BB8",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/bb8.png"))),
            new KeyValuePair<string,BitmapImage>("C-3PO",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/c3_po.png"))),
            new KeyValuePair<string,BitmapImage>("Chewbacca",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/chewie.png"))),
            new KeyValuePair<string,BitmapImage>("Tarfful",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/chewie.png"))),
            new KeyValuePair<string,BitmapImage>("Finn",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/finn.png"))),
            new KeyValuePair<string,BitmapImage>("General Hux",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/general_hux.png"))),
            new KeyValuePair<string,BitmapImage>("Han Solo",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/han_solo.png"))),
            new KeyValuePair<string,BitmapImage>("Kylo Ren",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/kylo_ren_color.png"))),
            new KeyValuePair<string,BitmapImage>("Leia Organa",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/leia_organa_solo.png"))),
            new KeyValuePair<string,BitmapImage>("Luke Skywalker",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/luke_skywalker.png"))),
            new KeyValuePair<string,BitmapImage>("Mas Amedda",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/mass.png"))),
            new KeyValuePair<string,BitmapImage>("Poe Dameron",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/poe_cameron.png"))),
            new KeyValuePair<string,BitmapImage>("R2-D2",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/r2_d2.png"))),
            new KeyValuePair<string,BitmapImage>("R5-D4",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/r5_d4.png"))),
            new KeyValuePair<string,BitmapImage>("R4-P17",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/r4_P17.png"))),
            new KeyValuePair<string,BitmapImage>("Rey",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/rey.png"))),
            new KeyValuePair<string,BitmapImage>("Captain Phasma",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/storm_truper.png"))),
            new KeyValuePair<string,BitmapImage>("Darth Vader",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/darth_vader.png"))),
            new KeyValuePair<string,BitmapImage>("Ackbar",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/admiral-akbar.png"))),
            new KeyValuePair<string,BitmapImage>("Yoda",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/yoda.png"))),
            new KeyValuePair<string,BitmapImage>("Jango Fett",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/jango_fett.png"))),
            new KeyValuePair<string,BitmapImage>("Boba Fett",new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/boba_fett.png"))),
        };

        private static readonly List<KeyValuePair<List<string>, BitmapImage>> _defaultImages =
            new List<KeyValuePair<List<string>, BitmapImage>>
            {
                new KeyValuePair<List<string>, BitmapImage>(new List<string> {"Sly Moore", "San Hill", "Wat Tambor", "Zam Wesell", "Poggle the Lesser","Bib Fortuna", "Nute Gunray","Greedo", "Wilhuff Tarkin", "Jabba Desilijic Tiure", "IG-88", "Bossk"}, new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/imperium.png"))),
                new KeyValuePair<List<string>, BitmapImage>(new List<string> {"Padmé Amidala", "Raymus Antilles", "Bail Prestor Organa", "Dormé", "Taun We", "Lama Su", "Dexter Jettster", "Cliegg Lars", "Cordé", "Gregar Typho", "Ben Quadinaros" ,"Gasgano","Dud Bolt","Ratts Tyerel","Shmi Skywalker","Quarsh Panaka","Sebulba", "Watto", "Ric Olié", "Rugor Nass","Roos Tarpals", "Jar Jar Binks", "Finis Valorum","Nien Nunb", "Wicket Systri Warrick", "Arvel Crynyd","Mon Mothma","Lobot","Lando Calrissian","Owen Lars", "Beru Whitesun lars", "Biggs Darklighter", "Wedge Antilles", "Jek Tono Porkins"}, new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/rebels.png"))),
                new KeyValuePair<List<string>, BitmapImage>(new List<string> { "Jocasta Nu", "Tion Medon", "Shaak Ti", "Barriss Offee", "Luminara Unduli", "Plo Koon", "Yarael Poof", "Saesee Tiin","Adi Gallia", "Eeth Koth","Kit Fisto", "Ki-Adi-Mundi","Mace Windu", "Ayla Secura", "Qui-Gon Jinn","Obi-Wan Kenobi", "Anakin Skywalker"}, new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/Jedi-icon.png"))),
                new KeyValuePair<List<string>, BitmapImage>(new List<string> (), new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/first_order.png"))),
                new KeyValuePair<List<string>, BitmapImage>(new List<string> {"Grievous", "Dooku", "Palpatine", "Darth Maul"}, new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/Sith.png"))),
            };
        //new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/first_order.png")),
        //    new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/rebels.png")),
        //    new BitmapImage(new Uri("ms-appx:///Assets/PeopleIcon/Jedi-icon.png"))

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var random = new Random();
            var person = value as People;
            if (person != null)
            {
                var image = _personImages.FirstOrDefault(item => item.Key.ContainsIgnoreCase(person.name));
                if (image.Value == null)
                {
                    var personTypeImage = _defaultImages.FirstOrDefault(item => item.Key.Contains(person.name));
                    if (personTypeImage.IsNotNull())
                    {
                        return personTypeImage.Value;
                    }
                }
                else
                {
                    return image.Value;
                }
            }
            return _defaultImages[random.Next(0, _defaultImages.Count - 1)].Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}