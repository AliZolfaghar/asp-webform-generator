Imports System.Drawing
Imports System.Data.SqlClient
Imports Dapper
Partial Class Admin_Manage_users
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    
    End Sub
    
    Protected Sub Button_Search_Click(sender As Object, e As EventArgs) Handles Button_Search.Click
        GridView_List.DataBind()
    End Sub
    
    Protected Sub Button_AddNew_Click(sender As Object, e As EventArgs) Handles Button_AddNew.Click
        Switch_To_Insert()
    End Sub
    Protected Sub Button_Edit_Click(sender As Object, e As EventArgs)
        Dim oSender As Button = sender
        Switch_To_Edit()
        Load_Data(oSender.CommandArgument)
    End Sub
    Protected Sub Button_SaveNew_Click(sender As Object, e As EventArgs) Handles Button_SaveNew.Click
        Save_New()
    End Sub
    Protected Sub Button_SaveChanges_Click(sender As Object, e As EventArgs) Handles Button_SaveChanges.Click
        Save_Changes()
    End Sub
    Protected Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Switch_To_List()
    End Sub
    Protected Sub Button_Delete_Click(sender As Object, e As EventArgs) Handles Button_Delete.Click
        Delete_Data()
    End Sub
    Sub Switch_To_List()
        Panel_list.Visible = True
        Panel_Edit.Visible = False
        Button_AddNew.Visible = True
    End Sub
    Sub Switch_To_Insert()
        Panel_list.Visible = False
        Panel_Edit.Visible = True
        Button_AddNew.Visible = False
        Label_EditTitle.Text = "افزودن"
        Button_SaveNew.Visible = True
        Button_SaveChanges.Visible = False
        Button_Cancel.Visible = True
        Button_Delete.Visible = False
        Label_Message.Text = ""
        '-> remove old data 
        TextBox_Str_RealName.Text = ""
        TextBox_Str_UserName.Text = ""
        TextBox_Str_PassWord.Text = ""
        TextBox_Str_Tel.Text = ""
        TextBox_Str_Email.Text = ""
        TextBox_Str_ACL.Text = ""
        CheckBox_Bol_UseDomain.Checked = False
        TextBox_Str_DomainName.Text = ""
        CheckBox_Bol_IsActive.Checked = False
    End Sub
    Sub Switch_To_Edit()
        Panel_list.Visible = False
        Panel_Edit.Visible = True
        Button_AddNew.Visible = False
        Label_EditTitle.Text = "ویرایش"
        Button_SaveNew.Visible = False
        Button_SaveChanges.Visible = True
        Button_Cancel.Visible = True
        Button_Delete.Visible = True
    End Sub
    Function Validate_Inputs() As Boolean
        Dim RV As Boolean = True
        Label_Message.ForeColor = Color.Red
        If TextBox_Str_RealName.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا نام و نام خانوادگی را وارد کنید."
            TextBox_Str_RealName.Focus()
            Return False
        End If

        If TextBox_Str_UserName.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا نام کاربری را وارد کنید."
            TextBox_Str_UserName.Focus()
            Return False
        End If

        If TextBox_Str_PassWord.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا کلمه عبور را وارد کنید."
            TextBox_Str_PassWord.Focus()
            Return False
        End If

        If TextBox_Str_Tel.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا تلفن تماس را وارد کنید."
            TextBox_Str_Tel.Focus()
            Return False
        End If

        If TextBox_Str_Email.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا آدرس پست الکترونیک را وارد کنید."
            TextBox_Str_Email.Focus()
            Return False
        End If

        If TextBox_Str_ACL.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا سطوح دسترسی را وارد کنید."
            TextBox_Str_ACL.Focus()
            Return False
        End If

        If TextBox_Str_DomainName.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا نام دامین را وارد کنید."
            TextBox_Str_DomainName.Focus()
            Return False
        End If

        Return RV
    End Function
    Sub Save_New()
        If Validate_Inputs() Then
            Dim SqlStr As String = "INSERT INTO dbo.TBL_AZ_Users (Str_RealName,Str_UserName,Str_PassWord,Str_Tel,Str_Email,Str_ACL,Bol_UseDomain,Str_DomainName,Bol_IsActive) VALUES (@Str_RealName,@Str_UserName,@Str_PassWord,@Str_Tel,@Str_Email,@Str_ACL,@Bol_UseDomain,@Str_DomainName,@Bol_IsActive) "
            Dim Params As Object = New With {  .Str_RealName = TextBox_Str_RealName.Text , .Str_UserName = TextBox_Str_UserName.Text , .Str_PassWord = TextBox_Str_PassWord.Text , .Str_Tel = TextBox_Str_Tel.Text , .Str_Email = TextBox_Str_Email.Text , .Str_ACL = TextBox_Str_ACL.Text , .Bol_UseDomain = CheckBox_Bol_UseDomain.Checked , .Str_DomainName = TextBox_Str_DomainName.Text , .Bol_IsActive = CheckBox_Bol_IsActive.Checked }
            Try 
                Using oCnn As New SqlConnection(CnnStr) 
                    oCnn.Execute(SqlStr, Params)
                End Using 
                Switch_To_List()
                GridView_List.DataBind()
                Label_ActionMessage.Text = "اطلاعات با موفقیت ثبت شد."
            Catch ex As Exception
                Label_Message.Text = "ثبت اطلاعات در بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
            End Try
        End If
    End Sub
    Sub Load_Data(ID As Integer)
        Dim SqlStr As String = "SELECT * FROM dbo.TBL_AZ_Users WHERE UserID=@UserID" 
        Dim Params As Object = New With { .UserID = ID }
        Try
            Using oCnn As New SqlConnection(CnnStr) 
                Dim Data = oCnn.QueryFirst(SqlStr, Params)
                Label_UserID.Text = Data.UserID
                TextBox_Str_RealName.Text = Data.Str_RealName
                TextBox_Str_UserName.Text = Data.Str_UserName
                TextBox_Str_PassWord.Text = Data.Str_PassWord
                TextBox_Str_Tel.Text = Data.Str_Tel
                TextBox_Str_Email.Text = Data.Str_Email
                TextBox_Str_ACL.Text = Data.Str_ACL
                CheckBox_Bol_UseDomain.Checked = Data.Bol_UseDomain
                TextBox_Str_DomainName.Text = Data.Str_DomainName
                CheckBox_Bol_IsActive.Checked = Data.Bol_IsActive
            End Using 
        Catch ex As Exception
            Label_Message.Text = ex.Message
        End Try
    End Sub
    Sub Save_Changes()
        If Validate_Inputs() Then
            Dim SqlStr As String = "UPDATE dbo.TBL_AZ_Users SET  Str_RealName=@Str_RealName , Str_UserName=@Str_UserName , Str_PassWord=@Str_PassWord , Str_Tel=@Str_Tel , Str_Email=@Str_Email , Str_ACL=@Str_ACL , Bol_UseDomain=@Bol_UseDomain , Str_DomainName=@Str_DomainName , Bol_IsActive=@Bol_IsActive WHERE UserID=@UserID "
            Dim Params As Object = New With { .UserID = Label_UserID.Text , .Str_RealName = TextBox_Str_RealName.Text , .Str_UserName = TextBox_Str_UserName.Text , .Str_PassWord = TextBox_Str_PassWord.Text , .Str_Tel = TextBox_Str_Tel.Text , .Str_Email = TextBox_Str_Email.Text , .Str_ACL = TextBox_Str_ACL.Text , .Bol_UseDomain = CheckBox_Bol_UseDomain.Checked , .Str_DomainName = TextBox_Str_DomainName.Text , .Bol_IsActive = CheckBox_Bol_IsActive.Checked              }
            Try
                Using oCnn As New SqlConnection(CnnStr)
                    oCnn.Execute(SqlStr, Params)
                End Using
                Switch_To_List()
                GridView_List.DataBind()
                Label_ActionMessage.Text = "اطلاعات با موفقیت به روز رسانی شد."
            Catch ex As Exception
                Label_Message.Text = "ثبت تغییرات در بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
            End Try
        End If
    End Sub
    Sub Delete_Data()
        Dim SqlStr As String = "DELETE FROM dbo.TBL_AZ_Users WHERE UserID=@UserID" 
        Dim Params As Object = New With { .UserID = Label_UserID.Text }
        Try
            Using oCnn As New SqlConnection(CnnStr)
                oCnn.Execute(SqlStr, Params)
            End Using
            Switch_To_List()
            GridView_List.DataBind()
            Label_ActionMessage.Text = "اطلاعات با موفقیت حذف شد."
        Catch ex As Exception
            Label_Message.Text = "حذف اطلاعات از بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
        End Try
    End Sub
End Class
