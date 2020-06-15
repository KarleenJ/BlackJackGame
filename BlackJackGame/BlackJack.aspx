<%@ Page Title="BlackJack" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BlackJack.aspx.cs" Inherits="BlackJackGame.BlackJack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="board">
     <asp:Table ID="table1" runat="server" Height="511px" Width="1331px"
        CellPadding="10" 
        GridLines="None"
        HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell ID="c11" Width="900">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Image ID="imgDealerCard1" runat="server" Height="240px" Width="160px" />
                    <asp:Image ID="imgDealerCard2" runat="server" Height="240px" Width="160px" />
                    <asp:Image ID="imgDealerCard3" runat="server" Height="240px" Width="160px" />
                    <asp:Image ID="imgDealerCard4" runat="server" Height="240px" Width="160px" />
                    <asp:Image ID="imgDealerCard5" runat="server" Height="240px" Width="160px" />                               
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell ID="c12">
                <asp:Label ID="lblBalance" runat="server" Font-Size="Large" Text="Player Balance:"></asp:Label>
                &nbsp;
                <asp:Label ID="lblBalanceAmt" runat="server" Font-Size="Large" Text=""></asp:Label>
                <br /><br />
                <asp:DropDownList ID="ddlBet" runat="server" Font-Size="Large" CssClass="auto-style3">
                   <asp:ListItem>1</asp:ListItem>
                   <asp:ListItem>2</asp:ListItem>
                   <asp:ListItem>5</asp:ListItem>
                   <asp:ListItem>25</asp:ListItem>
                   <asp:ListItem>50</asp:ListItem>
                   <asp:ListItem>75</asp:ListItem>
                   <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
                 &nbsp;
                <asp:Button ID="BtnBet" runat="server" Text="Bet" OnClick="btnBet_Click" />

                <br /><br />
                <asp:Label ID="lblDScore" Font-Size="Large" runat="server" Text="Dealer Score:" Visible="false"></asp:Label>
                 &nbsp;
                <asp:TextBox ID="tbDScore" Width="50px" runat="server" Visible="false" AutoPostBack="true"></asp:TextBox> 
                <br /><br />
                <asp:Label ID="lblPScore" Font-Size="Large" runat="server" Text="Player Score:" Visible="false"></asp:Label>
                 &nbsp;
                <asp:TextBox ID="tbPScore" Width="50px" runat="server" Visible="false" AutoPostBack="true"></asp:TextBox> 
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ID="c21" Width="900" >
                <asp:Panel ID="Panel2" runat="server">
                    <asp:Image ID="imgPlayerCard1" runat="server" Height="240px" Width="160px" />
                    <asp:Image ID="imgPlayerCard2" runat="server" Height="240px" Width="160px" />
                    <asp:Image ID="imgPlayerCard3" runat="server" Height="240px" Width="160px" />
                    <asp:Image ID="imgPlayerCard4" runat="server" Height="240px" Width="160px" />
                    <asp:Image ID="imgPlayerCard5" runat="server" Height="240px" Width="160px" />
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell ID="c22"> 
                <asp:Label ID="lblResult" runat="server" Text="" Visible="false" ForeColor="Red" Font-Size="Large"></asp:Label>  
                &nbsp;
                <asp:Button ID="BtnNewHand" runat="server" Text="New Hand!" Visible="false" OnClick="BtnNewHand_Click"/>
                <br /><br />
                <asp:Button ID="BtnHit"  runat="server" Text="HIT" OnClick="btnHit_Click" Visible="false"/>
                &nbsp;
                <asp:Button ID="BtnStay" runat="server" Text="STAY" OnClick="BtnStay_Click" Visible="false" />
                <br /><br />
                <asp:Button ID="BtnExit" runat="server" Text="Exit Game" OnClick="BtnExit_Click" Visible="false" />                
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
        </div>
</asp:Content>
