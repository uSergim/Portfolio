<%@ Page Language="VB" AutoEventWireup="true" CodeFile="support_page.aspx.vb" Inherits="support_page" %>

<!DOCTYPE html>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register TagPrefix="mn" Namespace="APNSoft.WebControls" Assembly="APNSoftMenu" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<%@ Import Namespace="NetChartTest" %>
<%@ Import Namespace="get_map_chart" %>
<%@ Import Namespace="main_window_functions" %>
<%@ Import Namespace="Menu.TwoInstances.CodeBehind.VB.NET" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 367px">
		////////////////////////////////////////////////////////////////////////BWTECH Netchart Support Page////////////////////////////////////////////////////////////////////////
		<br />
		<br />
		Select Technology: <select name='tech' id='tech'>
			<option value='<%=""%>'><%="Select a technology"%></option>
        <%For Each t_tech As String In Application("techsList").ToString.split(",") %>
          <option value='<%=t_tech %>'><%=t_tech%></option>
        <%Next%>
      </select>
		<br />
		<br />
		-----------------------------------------Share Menu-------------------------------------------------------------------------------------------------------<br />
        MENU OWNER: <input type="text" size="10" id='MENU_OWNER' name="MENU_OWNER" value="" maxlength="20" />
		MENU_ID: <input type="text" size="10" id='MENU_ID' name="MENU_ID" value="" maxlength="20" />
		USER TO SHARE WITH: <input type="text" size="10" id='menu_share_with' name="menu_share_with" value="" maxlength="20" />
		<asp:Button ID="ShareBtn" runat="server" OnClick="MenuShareBtn_Click" Text="SHARE MENU" /><br />
		--------------------------------------------------------------------------------------------------------------------------------------------------------------
		<br />
		<br />
		-----------------------------------------Share KPI--------------------------------------------------------------------------------------------------------<br />
		KPI OWNER: <input type="text" size="10" id='KPI_OWNER' name="KPI_OWNER" value="" maxlength="20" />
		KPI_ID: <input type="text" size="10" id='KPI_ID' name="KPI_ID" value="" maxlength="40" />
		USER TO SHARE WITH: <input type="text" size="10" id='kpi_share_with' name="kpi_share_with" value="" maxlength="20" />
		<asp:Button ID="Button1" runat="server" OnClick="KPIShareBtn_Click" Text="SHARE KPI" /><br />
		--------------------------------------------------------------------------------------------------------------------------------------------------------------
		<br />
		<br />
		-----------------------------------------Update Data------------------------------------------------------------------------------------------------------<br />
		<asp:Button ID="Button2" runat="server" OnClick="UpdateDatesBtn_Click" Text="UPDATE DATES" />
		<asp:Button ID="Button3" runat="server" OnClick="UpdateRegionsBtn_Click" Text="UPDATE REGIONS" /> <br />
		--------------------------------------------------------------------------------------------------------------------------------------------------------------
		<br />
		<br />
		-----------------------------------------Configure New Technology------------------------------------------------------------------------------------<br />
		<asp:Button ID="Button4" runat="server" OnClick="CreateNewTechTablesBtn_Click" Text="CREATE NEW TECH DEFAULT TABLES" /><br />
		---------------------------------------------------------------------------------------------------------------------------------------------------------------
		<br />
		<br />
		<br />
		<br />
		<a href = "default.aspx" ><- Back to Netchart</a>
    </div>
    </form>
</body>
</html>