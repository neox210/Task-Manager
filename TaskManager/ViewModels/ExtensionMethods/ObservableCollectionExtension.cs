﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TaskManager.ViewModels.ExtensionMethods
{
    public static class ObservableCollectionExtension
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if(collection == null) throw new NullReferenceException();
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
