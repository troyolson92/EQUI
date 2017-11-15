using System;
using Microsoft.SharePoint.Client;
using System.Security;

namespace EQUICommunictionLib
{
    public class Sharepoint
    {
        //login to website build credentials
        public SharePointOnlineCredentials onlineCredentials()
        {
            string login = "e6308159@volvocars.com"; //give your username here  
            string password = "e6308159"; //give your password  
            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            return new SharePointOnlineCredentials(login, securePassword);
        }
        //buld clientcontext (user session to work with)
        public ClientContext clientContext()
        {
            string siteUrl = "https://sharepoint.volvocars.net/sites/maintenance_a_shop";
            ClientContext clientContext = new ClientContext(siteUrl);
            clientContext.Credentials = onlineCredentials();
            return clientContext;
        }

        public void TestGetList()
        {
            ClientContext client = clientContext();

            Microsoft.SharePoint.Client.List myList = client.Web.Lists.GetByTitle("IssueTrackTest");
            ListItemCreationInformation itemInfo = new ListItemCreationInformation();

            //get fieldlist
            FieldCollection fieldColl = myList.Fields;         
            client.Load(fieldColl);
            client.ExecuteQuery();
            foreach (var f in fieldColl)
            {
                if (f.Hidden == false)
                {
                    Console.WriteLine(f.InternalName);
                }
            }

            //get the list items
            CamlQuery query = new CamlQuery();
            query.ViewXml = ""; // "<View><Query><Where><Contains><FieldRef Name='Title'/><Value Type='Text'>announce</Value></Contains></Where></Query></View>";
            ListItemCollection collListItem = myList.GetItems(query);
            client.Load(collListItem);
            client.ExecuteQuery();

            if (collListItem.Count == 0)
            {
                Console.WriteLine("No items found.");
            }
            else
            {
                Console.WriteLine("Items found:\n");
                ShowData(collListItem, fieldColl);
            }
        }

        private void ShowData(ListItemCollection listItemCollection, FieldCollection fieldCollection)
        {
            foreach (ListItem targetListItem in listItemCollection)
            {
                Console.WriteLine(targetListItem["Title"]);
                //loop fields 
                foreach (var f in fieldCollection)
                {
                    if (!f.Hidden)
                    {
                        try
                        {
                            Console.WriteLine(string.Format("Field: {0} Value: {1}", f.InternalName, targetListItem[f.InternalName]));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }

        public void TestGetView()
        {
            ClientContext client = clientContext();
            Microsoft.SharePoint.Client.List myList = client.Web.Lists.GetByTitle("IssueTrackTest");
            client.Load(myList);
            client.ExecuteQuery();
            View view = myList.Views.GetByTitle("All Issues");

            client.Load(view);
            client.ExecuteQuery();
            CamlQuery query = new CamlQuery();
            query.ViewXml = view.ViewQuery;

            ListItemCollection items = myList.GetItems(query);
            client.Load(items);
            client.ExecuteQuery();
            Console.WriteLine("done found: " + items.Count);
        }

        public void AddNewIusse(string location, string title, string refURL)
        {
                ClientContext client = clientContext();

                Microsoft.SharePoint.Client.List myList = client.Web.Lists.GetByTitle("Whiteboard Issues");
                ListItemCreationInformation itemInfo = new ListItemCreationInformation();
                ListItem myItem = myList.AddItem(itemInfo);

                myItem["Locatie"] = location;
                myItem["Title"] = title;
                //myItem["Comments"] = "wat extra info";

                Microsoft.SharePoint.Client.FieldUrlValue fieldUrlValue = new FieldUrlValue();
                fieldUrlValue.Url = refURL;
                fieldUrlValue.Description = "EventData";
                myItem["EventReferentie"] = fieldUrlValue;

                myItem.Update();
                client.ExecuteQuery();
        }
    }
}