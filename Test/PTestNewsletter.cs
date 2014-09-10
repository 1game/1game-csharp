using System;
using System.Collections;
using OneGame;

namespace OneGameTest
{
	internal class PTestNewsletter : PTest 
	{
		public static void Subscribe(Action done) 
		{
			const string section = "PTestNewsletter.Subscribe";
			Console.WriteLine (section);

			var options = new Hashtable
			{
				{"email", "invalid @email.com"}
			};

            OneGame.Newsletter.Subscribe(options, r =>
            {
				AssertFalse(section + "#1", "Request failed", r.success);
				AssertEquals(section + "#1", "Mailchimp API error", r.errorcode, 602);

				options["email"] = "valid@testuri.com";
				options["fields"] = new Hashtable { 
					{"STRINGVALUE", "this is a string"}
				};

                OneGame.Newsletter.Subscribe(options, r2 =>
                {
					AssertTrue(section, "Request succeeded", r2.success);
					AssertEquals(section, "No errorcode", r2.errorcode, 0);
					done();
				});
			});
		}
	}
}
