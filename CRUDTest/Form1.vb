Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    Dim i As Integer

    'form load event
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'set the connection string
        conn = New MySqlConnection("server=127.0.0.1;userid=root;password='';database=crud")
        Try
            conn.Open()
            MessageBox.Show("Connection to MySQL test database was successful!!!!", "TESTING CONNECTION TO MySQL DATABASE")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        DGV_Load()
    End Sub

    Public Sub DGV_Load()
        Try
            conn.Open()
            cmd = New MySqlCommand("SELECT * FROM `tbl_crud`", conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        save()
    End Sub

    Public Sub save()
        Try
            conn.Open()
            cmd = New MySqlCommand("INSERT INTO `tbl_crud`(`PRODUCTNO`, `PRODUCTNAME`, `PRICE`, `PRODGROUP`, `EXPDATE`, `STATUS`) VALUES (@PRODUCTNO, @PRODUCTNAME, @PRICE, @PRODGROUP, @EXPDATE, @STATUS)", conn)
            cmd.Parameters.AddWithValue("@PRODUCTNO", txt_ProductNo.Text)
            cmd.Parameters.AddWithValue("@PRODUCTNAME", txt_ProductName.Text)
            cmd.Parameters.AddWithValue("@PRICE", CDec(txt_Price.Text))
            cmd.Parameters.AddWithValue("@PRODGROUP", cb_ProdGroup.Text)
            cmd.Parameters.AddWithValue("@EXPDATE", CDate(dtp_Expire.Value))
            cmd.Parameters.AddWithValue("@STATUS", CBool(cb_Available.Checked.ToString))
            i = cmd.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Record has been saved successfully!!!")
                DGV_Load() ' Refresh DataGridView
            Else
                MsgBox("Record has not been saved successfully!!!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btn_Update_Click(sender As Object, e As EventArgs) Handles btn_Update.Click
        updateRecord()
    End Sub

    Public Sub updateRecord()
        Try
            conn.Open()
            cmd = New MySqlCommand("UPDATE `tbl_crud` SET `PRODUCTNAME`=@PRODUCTNAME, `PRICE`=@PRICE, `PRODGROUP`=@PRODGROUP, `EXPDATE`=@EXPDATE, `STATUS`=@STATUS WHERE `PRODUCTNO`=@PRODUCTNO", conn)
            cmd.Parameters.AddWithValue("@PRODUCTNO", txt_ProductNo.Text)
            cmd.Parameters.AddWithValue("@PRODUCTNAME", txt_ProductName.Text)
            cmd.Parameters.AddWithValue("@PRICE", CDec(txt_Price.Text))
            cmd.Parameters.AddWithValue("@PRODGROUP", cb_ProdGroup.Text)
            cmd.Parameters.AddWithValue("@EXPDATE", CDate(dtp_Expire.Value))
            cmd.Parameters.AddWithValue("@STATUS", CBool(cb_Available.Checked.ToString))
            i = cmd.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Record has been updated successfully!!!")
                DGV_Load() ' Refresh DataGridView
            Else
                MsgBox("Record has not been updated successfully!!!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btn_Delete_Click(sender As Object, e As EventArgs) Handles btn_Delete.Click
        'create delete code here

        Try
            conn.Open()
            cmd = New MySqlCommand("DELETE FROM `tbl_crud` WHERE `PRODUCTNO`=@PRODUCTNO", conn)
            cmd.Parameters.AddWithValue("@PRODUCTNO", txt_ProductNo.Text)
            i = cmd.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Record has been deleted successfully!!!")
                DGV_Load() ' Refresh DataGridView
            Else
                MsgBox("Record has not been deleted successfully!!!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Add the CellClick event handler
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txt_ProductNo.Text = row.Cells("PRODUCTNO").Value.ToString()
            txt_ProductName.Text = row.Cells("PRODUCTNAME").Value.ToString()
            txt_Price.Text = row.Cells("PRICE").Value.ToString()
            cb_ProdGroup.Text = row.Cells("PRODGROUP").Value.ToString()
            dtp_Expire.Value = Convert.ToDateTime(row.Cells("EXPDATE").Value)
            cb_Available.Checked = Convert.ToBoolean(row.Cells("STATUS").Value)
        End If
    End Sub
End Class
