﻿using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Couche_Data.Dao
{
    public class StatAccountDAO : IStatAccountDAO
    {

        public List<StatAccount> GetStat(int semaine = 0, int mois = 0, int annee = 0)
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string stm = $"SELECT acompte_id,sum(amount) as argent FROM best_acomptes WHERE WEEK(date) = WEEK(CURRENT_DATE) + {semaine} AND " +
                    $"MONTH(date) = MONTH(CURRENT_DATE) + {mois} AND" +
                    $"YEAR(date) = YEAR(CURRENT_DATE) + {annee} group by acompte_id order by argent desc";
             
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatAccount> statAccountList = new List<StatAccount>();
                while (rdr.Read())
                {
                    statAccountList.Add(new StatAccount(0, DateTime.Now, rdr.GetFloat("argent"), rdr.GetInt32("acompte_id")));
                }

                rdr.Close();
                sql.Close();
                return statAccountList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats acompte : {ex.Message}");
                return new();
            }
        }

        public void CreateStat(StatAccount stat)
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string formattedDate = stat.Date.ToString("yyyy-MM-dd");
                string formattedAmountMoney = stat.Money.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string stm = $"INSERT INTO best_acomptes VALUES(0,{formattedAmountMoney},'{formattedDate}',{stat.Account_Id})";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                cmd.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant l'ajout d'une stat acompte : {ex.Message}");
            }
        }
    }
}
