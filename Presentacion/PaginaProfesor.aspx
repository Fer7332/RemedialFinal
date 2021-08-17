<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaginaProfesor.aspx.cs" Inherits="Presentacion.PaginaProfesor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Actualizar Datos" />
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList3" runat="server" Height="16px" Width="172px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnasignaprof" runat="server" OnClick="btnasignaprof_Click1" Text="Asignar Profesor a Materia" Width="197px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Grado Especialidad Profesor" />
            <br />
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Text="Registro Empleado"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtregistroempl" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Text="Nombre"></asp:Label>
            &nbsp;<asp:TextBox ID="txtnombre" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Text="Primer Apellido"></asp:Label>
            &nbsp;<asp:TextBox ID="txtap" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Text="Segundo Apellido"></asp:Label>
            <asp:TextBox ID="txtam" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Text="Género"></asp:Label>
            <asp:TextBox ID="txtgenero" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Text="Categoria"></asp:Label>
            <asp:TextBox ID="txtcategoria" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Text="Correo"></asp:Label>
            <asp:TextBox ID="txtcorreo" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Text="Celular"></asp:Label>
            <asp:TextBox ID="txtcelular" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label10" runat="server" Font-Names="Arial" Text="Estado"></asp:Label>
            &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="120px">
            </asp:DropDownList>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btninsertar" runat="server" OnClick="btninsertar_Click" Text="Insertar" />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnmodificar" runat="server" OnClick="btnmodificar_Click" Text="Modificar Tabla" />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="labaux" runat="server"></asp:Label>
            <br />
            &nbsp;*Para eliminar un Profesor Primero se debe eliminar del PerfilProfesor<br />
&nbsp;<asp:Button ID="btneliminar" runat="server" OnClick="btneliminar_Click" Text="Eliminar" />
            &nbsp;<asp:DropDownList ID="DropDownList2" runat="server" Height="20px" Width="177px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Button ID="btnmostrar" runat="server" OnClick="btnmostrar_Click" Text="Mostrar" />
            <br />
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
