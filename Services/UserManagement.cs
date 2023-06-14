using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReference.Services;

public partial class UserManagement
{
    public UserManagement()
    {
        // Vérifier si la DataTable "Users" existe déjà dans le DataSet
        DataTable userTable = Globals.UserSets.Tables["Users"];
        if (userTable == null)
        {
            // La DataTable n'existe pas, créer une nouvelle DataTable et l'ajouter au DataSet
            userTable = new DataTable();

            DataColumn user_ID = new DataColumn("User_ID", typeof(short));
            DataColumn userName = new DataColumn("UserName", typeof(string));
            DataColumn userPassword = new DataColumn("UserPassword", typeof(string));
            DataColumn accessType = new DataColumn("UserAccessType", typeof(short));

            userTable.TableName = "Users";
            user_ID.Unique = true;
            user_ID.AutoIncrement = true;
            userTable.Columns.Add(user_ID);

            userName.Unique = true;
            userTable.Columns.Add(userName);

            userPassword.Unique = false;
            userTable.Columns.Add(userPassword);

            accessType.Unique = false;
            userTable.Columns.Add(accessType);

            // Ajouter la DataTable nouvellement créée au DataSet
            Globals.UserSets.Tables.Add(userTable);
        }

        // Vérifier si la DataTable "Access" existe déjà dans le DataSet
        DataTable accessTable = Globals.UserSets.Tables["Access"];
        if (accessTable == null)
        {
            // La DataTable n'existe pas, créer une nouvelle DataTable et l'ajouter au DataSet
            accessTable = new DataTable();

            DataColumn access_ID = new DataColumn("Access_ID", typeof(short));
            DataColumn accessName = new DataColumn("AccessName", typeof(string));
            DataColumn createObject = new DataColumn("CreateObject", typeof(bool));
            DataColumn destroyObject = new DataColumn("DestroyObject", typeof(bool));
            DataColumn modifyObject = new DataColumn("ModifyObject", typeof(bool));
            DataColumn changeUserRights = new DataColumn("ChangeUserRights", typeof(bool));

            accessTable.TableName = "Access";
            access_ID.Unique = true;
            accessTable.Columns.Add(access_ID);

            accessName.Unique = true;
            accessTable.Columns.Add(accessName);

            createObject.Unique = false;
            accessTable.Columns.Add(createObject);

            destroyObject.Unique = false;
            accessTable.Columns.Add(destroyObject);

            modifyObject.Unique = false;
            accessTable.Columns.Add(modifyObject);

            changeUserRights.Unique = false;
            accessTable.Columns.Add(changeUserRights);

            // Ajouter la DataTable nouvellement créée au DataSet
            Globals.UserSets.Tables.Add(accessTable);
        }

        // Vérifier si la relation "Access2User" existe déjà dans le DataSet
        DataRelation relation = Globals.UserSets.Relations["Access2User"];
        if (relation == null)
        {
            // La relation n'existe pas, créer une nouvelle relation entre les colonnes correspondantes
            DataColumn parentColumn = Globals.UserSets.Tables["Access"].Columns["Access_ID"];
            DataColumn childColumn = Globals.UserSets.Tables["Users"].Columns["UserAccessType"];

            relation = new DataRelation("Access2User", parentColumn, childColumn);

            // Ajouter la relation au DataSet
            Globals.UserSets.Relations.Add(relation);
        }
    }

  public bool CheckCredentials(string login, string password)
    {
        try
        {
          DataTable usersTable = Globals.UserSets.Tables["Users"];

         
           DataRow[] matchingRows = usersTable.Select($"UserName = '{login}' AND UserPassword = '{password}'");

          if (matchingRows.Length > 0)
           {
               return true;
           }
        }
       catch (Exception ex)
       {
           Shell.Current.DisplayAlert("Erreur", ex.Message, "OK").Wait();
       }

       return false; 
    }
}



