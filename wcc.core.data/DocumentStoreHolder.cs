﻿using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.core.data
{
    // 'DocumentStore' is a main-entry point for client API.
    // It is responsible for managing and establishing connections
    // between your application and RavenDB server/cluster
    // and is capable of working with multiple databases at once.
    // Due to it's nature, it is recommended to have only one
    // singleton instance per application
    public static class DocumentStoreHolder
    {
        private static string? _connectionString;
        public static void Init(string connectionString)
        {
            _connectionString = connectionString;
        }

        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Urls = new[] { _connectionString },
                    Database = "wcc.core"
                };

                return store.Initialize();
            });

        internal static IDocumentStore Store => LazyStore.Value;
    }
}
