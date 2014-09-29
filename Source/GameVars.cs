using System;
using System.Collections;

namespace OneGame
{
	public class GameVars
	{
		private const string SECTION = "gamevars";
        private const string LOAD = "load";
        private const string LOADSINGLE = "single";
		private const string SAVE = "save";
			
		/**
		 * Loads all GameVars
		 * @param	callback	Action<Hashtable, PResponse>	Your callback method
		 */
		public static void Load(Action<Hashtable, PResponse> callback)
		{
			PRequest.GetResponse (SECTION, LOAD, null, response => {
				var data = response.success ? response.json : null;
				callback(data, response);
			});
		}

		/**
		 * Loads a single GameVar
		 * @param	name	string	The variable name to load
		 * @param	callback	Action<Hashtable, PResponse>	Your callback method
		 */
		public static void LoadSingle(string name, Action<Hashtable, PResponse> callback)
		{
			var postdata = new Hashtable
			{
				{"name", name}
			};

			PRequest.GetResponse (SECTION, LOADSINGLE, postdata, response => {
				var data = response.success ? response.json : null;
				callback(data, response);
			});
		}

		/**
		 * Saves a single GameVar
		 * @param	name	string	The variable name to save
         * @param   value   string  The value associated to this variable
         * @param   overwrite bool  If the variable exists, should it be overwritten
		 * @param	callback	Action<Hashtable, PResponse>	Your callback method
		 */
		public static void Save(string name, string value, bool overwrite, Action<Hashtable, PResponse> callback)
		{
            var postdata = new Hashtable
			{
				{"name", name},
                {"value", value},
                {"overwrite", overwrite}
			};

			PRequest.GetResponse (SECTION, SAVE, postdata, response => {
				var data = response.success
                    ? response.json["variables"] is System.Collections.Hashtable
                        ? (System.Collections.Hashtable)response.json["variables"]
                        : null
                    : null;
				callback(data, response);
			});
		}
	}
}