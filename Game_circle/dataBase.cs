using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Game_circle
{
	internal class dataBase
	{
		public MySqlConnection connection;
		public MySqlCommand cmd;

		public dataBase()
		{
			MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=db-107");
			connection.Open();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "CREATE TABLE IF NOT EXISTS `Score` (" +
				"id SERIAL PRIMARY KEY, " +
				"Color varchar(15), " +
				"Win int, " +
				"Loose int, " +
				"Void int, " +
				"Score int)";          
				//cmd = "CREATE TABLE IF EXISTS Score";
			cmd.Connection = connection;
			cmd.ExecuteNonQuery();

		}

		public void Add_Element()
		{
			//this.cmd = 
		}


		
	}
}
