using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using mobile_poverka_asset.Services;
using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;

namespace mobile_poverka_asset.Database
{
    class db_search2_modifypribor
    {
        public async static Task<bool> ModifyPribor()
        {
            return await Task.FromResult( false);
        }
    }
}
