﻿using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace nexBart.ViewModels
{
    public class FavoritesModel
    {
        public ObservableCollection<Station> FavoriteStations {get; set;}

        public FavoritesModel()
        {
            FavoriteStations = new ObservableCollection<Station>();
        }

        public async Task RefreshFavorites()
        {
            List<Station> favorites = await DatabaseHelper.GetFavorites();
            List<Line> lines = new List<Line>();

            for (int i = 0; i < favorites.Count; i++ )
            {
                lines = await WebHelper.GetPredictions(new StationData(favorites[i].Name, favorites[i].Abbrv));
                favorites[i].AddLineList(lines);
                //FavoriteStations.Add(s);
            }

            foreach(Station x in favorites)
            {
                FavoriteStations.Add(x);
            }
        }

        public async Task AddFavorite(Station favorite)
        {
            await DatabaseHelper.AddFavorite(favorite);
        }

        public async Task CheckFavorites()
        {
            await DatabaseHelper.CheckDB();
        }

        public bool IsFavorite(Station selection)
        {
            int index = FavoriteStations.IndexOf(selection);

            return FavoriteStations.Any(s => s.Name == selection.Name);
        }
    }
}
