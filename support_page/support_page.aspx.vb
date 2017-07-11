'Developer: Sergio Chequer Saraiva
'Company: BWTECH
'This is an example of my code style. 
'This simple page was created to help the support team perform their job in their daily routine.
'Please note that some references were changed so that the software security is not compromised.

Imports System.IO

Partial Class support_page
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
		If Not ValidateUser() Then
			HttpContext.Current.Response.Redirect("DEFAULTPAGE.aspx")
		End If
		
	End Sub

	Protected Sub MenuShareBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		ShareMenu()
	End Sub

	Protected Sub KPIShareBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		ShareKPI()
	End Sub

	Protected Sub UpdateDatesBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		UpdateDates()		
	End Sub

	Protected Sub UpdateRegionsBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		UpdateRegions()
	End Sub

	Protected Sub CreateNewTechTablesBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		CreateTechTables()
	End Sub

	''' <summary>
	''' This page can only be accessed if the user has logged in previously and works by the company.
	''' </summary>
	''' <returns>True if user has permission</returns>
	''' <remarks>Created By Sergio C.S. on 2017-01-28</remarks>
	Protected Function ValidateUser() As Boolean
		Dim v_connection As New ConnectionManager(ConnectionManager.typesConnection.USERS)
		Dim v_command As New Data.Odbc.OdbcCommand
		Dim v_dataReader As Data.Odbc.OdbcDataReader
		Dim v_project As Projects = New Projects(Application("techsList").ToString.Split(",")(0))
		Dim v_user As String = common_functions.validateuser()
		Dim v_company As String = ""

		Try
		Using v_connection
			v_command.CommandText = "SELECT company FROM " & common_functions.getGenericDBName() & ".USERSTABLE" & _
			" WHERE username = '" & v_user & "'"
			v_dataReader = v_connection.executeReader(v_command)

			Using v_dataReader
				v_dataReader.Read()
				v_company = v_dataReader(0).ToString
				v_dataReader.Close()
			End Using
		End Using
		Catch ex As Exception
			Return False
		End Try

		Return v_company.ToLower.Equals("COMPANYNAME")

	End Function

	''' <summary>
	''' Checks if user have selected at least one technology.
	''' </summary>
	''' <param name="p_tech">Tech name</param>
	''' <returns>True if technology is accepted. False otherwise.</returns>
	''' <remarks>Created By Sergio C.S. on 2017-01-28</remarks>
	Protected Function ValidateTech(p_tech As String) As Boolean
		If p_tech.Equals("") Then
			HttpContext.Current.Response.Write("<font color=""red"">No Technology Selected!!</font> <br><br>")
			Return False
		Else
			Return True
		End If
	End Function
	
	''' <summary>
	''' Shares a user's private menu entry with another user.
	''' </summary>
	''' <remarks>Created By Sergio C.S. on 2017-01-28</remarks>
	Protected Sub ShareMenu()
		Dim v_tech As String = HttpContext.Current.Request.Form.Item("tech")
		If Not ValidateTech(v_tech) Then Exit Sub

		Dim v_menuOwner As String = HttpContext.Current.Request.Form.Item("MENU_OWNER")
		Dim v_menuId As String = HttpContext.Current.Request.Form.Item("MENU_ID")
		Dim v_userToShare As String = HttpContext.Current.Request.Form.Item("menu_share_with")
		Dim v_path As String
		Dim v_pathArray As String()
		Dim v_project As Projects = New Projects(v_tech)
		Dim v_connection As New ConnectionManager(ConnectionManager.typesConnection.USERS)
		Dim v_command As New Data.Odbc.OdbcCommand
		Dim v_dataReader As Data.Odbc.OdbcDataReader
		Try
			Using v_connection : Using v_command :	Using v_dataReader 

				v_command.CommandText = "SELECT PATH FROM " & v_project.DbName("MENUTABLE", Projects.DataBaseType.netchart, False) & _
										" WHERE `owner` = '" & v_menuOwner & "' AND MENU_ID = " & v_menuId
				v_dataReader = v_connection.ExecuteReader(v_command)
				v_dataReader.Read()
				v_path = v_dataReader(0).ToString
				v_dataReader.Close()
				
				v_command.CommandText = "INSERT IGNORE INTO " & v_project.DbName("SHAREDMENUTABLE", Projects.DataBaseType.netchart, False) & _
					"VALUES ('" & v_menuId & "','" & v_userToShare & "');"
				v_connection.executeNonQuery(v_command)
			
				'insert whole hierarchy
				v_pathArray = Split(v_path,"ch")
				Dim v_newMenuID As String
				Dim v_newPath As String = ""
				Dim v_index As Integer = 0

				For i = 0 To v_pathArray.Length - 2
					For j = 0 to v_index
						if j > 0 Then
							v_newPath &= "ch" & v_pathArray(j)
						Else
							v_newPath &= v_pathArray(j)
						End If
					Next
					v_index+=1

					v_command.CommandText = "SELECT MENU_ID FROM " & v_project.DbName("MENUTABLE", Projects.DataBaseType.netchart, False) & _
										" WHERE `owner` = '" & v_menuOwner & "' AND PATH = '" & v_newPath & "'"
					v_dataReader = v_connection.ExecuteReader(v_command)
					v_dataReader.Read()
					v_newMenuID = v_dataReader(0).ToString
					v_dataReader.Close()

					v_command.CommandText = "INSERT IGNORE INTO " & v_project.DbName("SHAREDMENUTABLE", Projects.DataBaseType.netchart, False) & _
						"VALUES ('" & v_newMenuID & "','" & v_userToShare & "');"
					v_connection.ExecuteNonQuery(v_command)

				Next
			End Using :	End Using :	End Using
		Catch ex As Exception
			HttpContext.Current.Response.Write("<font color=""red"">An error occurred while sharing this menu entry.</font> <br><br>")
			Exit Sub
		End Try
		HttpContext.Current.Response.Write("<font color=""blue"">Menu entry shared successfully!!</font> <br><br>")
	End Sub

	''' <summary>
	''' Shares a user's private KPI with another user.
	''' </summary>
	''' <remarks>Created By Sergio C.S. on 2017-01-28</remarks>
	Protected Sub ShareKPI() 
		Dim v_tech As String = HttpContext.Current.Request.Form.Item("tech")
		If Not ValidateTech(v_tech) Then Exit Sub

		Dim v_kpiOwner As String = HttpContext.Current.Request.Form.Item("KPI_OWNER")
		Dim v_userToShare As String = HttpContext.Current.Request.Form.Item("kpi_share_with")
		Dim v_kpiID As String = HttpContext.Current.Request.Form.Item("KPI_ID")
		Dim v_project As Projects = New Projects(v_tech)
		Dim v_connection As New ConnectionManager(ConnectionManager.typesConnection.USERS)
		Dim v_command As New Data.Odbc.OdbcCommand

		Using v_connection : Using v_command
		v_command.CommandText = "INSERT IGNORE INTO " & v_project.DbName("KPITABLE", Projects.DataBaseType.netchart, False) & _
			"VALUES ('" & v_userToShare & "','" & v_kpiOwner & "','" & v_kpiID & "');"
		
			Try
				v_connection.executeNonQuery(v_command)
			Catch ex As Exception
				HttpContext.Current.Response.Write("<font color=""red"">An error occurred while sharing this KPI.</font> <br><br>")
				Exit Sub
			End Try
		End Using : End Using

		HttpContext.Current.Response.Write("<font color=""blue"">KPI shared successfully!!</font> <br><br>")

	End Sub

	''' <summary>
	''' Performs a request to update Netchart's available dates for CM and PM.
	''' </summary>
	''' <remarks>Created By Sergio C.S. on 2017-01-28</remarks>
	Protected Sub UpdateDates()
		Dim v_tech As String = HttpContext.Current.Request.Form.Item("tech")
		Dim v_url As String
		If Not ValidateTech(v_tech) Then Exit Sub

		HttpContext.Current.Response.Write("<font color=""blue"">Updating dates...</font> <br><br>")

		v_url = "doAjax.aspx?PARAMTERSTORUN=FUNCTION&tech=" & v_tech
		HttpContext.Current.Server.Execute(v_url)
	End Sub

	''' <summary>
	''' Performs a request to update Netchart's regions.
	''' </summary>
	''' <remarks>Created By Sergio C.S. on 2017-01-28</remarks>
	Protected Sub UpdateRegions()
		Dim v_tech As String = HttpContext.Current.Request.Form.Item("tech")
		Dim v_url As String
		If Not ValidateTech(v_tech) Then Exit Sub

		HttpContext.Current.Response.Write("<font color=""blue"">Updating regions...</font> <br><br>")

		v_url = "doAJAX.aspx?PARAMTERSTORUN=FUNCTION&tech=" & v_tech
		HttpContext.Current.Server.Execute(v_url)
	End Sub

	''' <summary>
	''' Creates all necessary tables for any technology in Netchart (only inside the technology database).
	''' </summary>
	''' <remarks>Created By Sergio C.S. on 2017-01-28</remarks>
	Protected Sub CreateTechTables()
		Dim v_tech As String = HttpContext.Current.Request.Form.Item("tech")
		If Not ValidateTech(v_tech) Then Exit Sub
		
		Dim v_project As New Projects(v_tech)
		Dim v_fileData As String
		Dim v_tablesNotCreated As String = ""
		Dim v_tablesCreated As String = ""
		Dim v_query As String = ""
		Dim v_fileDataArray As String()
		Dim v_applicationAbsolutePath As String = HttpContext.Current.Request.ServerVariables("APPL_PHYSICAL_PATH")

		HttpContext.Current.Response.Write("<font color=""blue"">Creating " & v_tech & " default tables...</font> <br><br>")

		'CREATE TABLES
		Using v_streamReader As New StreamReader(v_applicationAbsolutePath & "PATH\CREATE_TABLES.sql")
			v_fileData = v_streamReader.ReadToEnd()
			v_streamReader.Close()
		End Using
		v_fileDataArray = v_fileData.Split(";")

		Using v_connection As New ConnectionManager(ConnectionManager.typesConnection.USERS) : Using v_command As New Data.Odbc.OdbcCommand 
			For Each t_tableCommand In v_fileDataArray
					If t_tableCommand.Trim().Length = 0 Then Continue For
					Dim v_i As Integer = t_tableCommand.IndexOf("`")
					Dim v_tableName As String = t_tableCommand.Substring(v_i + 1, t_tableCommand.IndexOf("`", v_i + 1) - v_i - 1)
					Try
						t_tableCommand = t_tableCommand.Trim().Insert(13, v_project.GetDatabaseName(Projects.DataBaseType.netchart,False,False) & ".")
						v_command.CommandText = t_tableCommand
						v_connection.executeNonQuery(v_command)
						v_tablesCreated &=  v_tableName & ","
					Catch ex As Exception
						v_tablesNotCreated &= v_tableName & ","
					End Try
			Next
		
			'POPULATE NETCHART_MENU
			If v_tablesCreated.Contains("MENUTABLE") Then
				Using v_streamReader As New StreamReader(v_applicationAbsolutePath & "PATH\menufile.txt")
				v_fileData = v_streamReader.ReadLine()
				v_query = "INSERT IGNORE INTO " & v_project.GetDatabaseName(Projects.DataBaseType.netchart,False,True) & ".`MENUTABLE`(" & v_fileData & ") VALUES"
				While Not v_streamReader.EndOfStream
					v_fileData = v_streamReader.ReadLine()
					v_query &= "(" & v_fileData & "),"
				End While
				v_streamReader.Close()
				End Using
				common_functions.RemoveLastChar(v_query)
				Try
					v_command.CommandText = v_query
					v_connection.executeNonQuery(v_command)
					HttpContext.Current.Response.Write("<font color=""blue"">MENUTABLE successfully populated!</font> <br>")
				Catch ex As Exception
					HttpContext.Current.Response.Write("<font color=""red"">MENUTABLE was not populated!</font> <br>")
				End Try
			End If
			
		End Using : End Using

		HttpContext.Current.Response.Write("<font color=""blue"">The following tables were created: "& v_tablesCreated & "</font> <br>")
		HttpContext.Current.Response.Write("<font color=""red"">The following tables were NOT created: "& v_tablesNotCreated & "</font> <br><br>")
	End Sub

End Class
