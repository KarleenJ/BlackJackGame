<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BlackJackGame._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Click below to start a new game</h1>
        <asp:Button ID="Button1" runat="server" Text="Start Game" width="200" height="200" Font-Size="X-Large" OnClick="Button1_Click"/>
        <p class="lead">        
    </div>

</asp:Content>
