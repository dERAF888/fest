using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace fest
{
    public partial class bilet : Form
    {
        db db = new db();
        public bilet()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            db.OpenConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from Покупка", db.getConnection);
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter("Select Имя from Покупка", db.getConnection);
            SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("Select Фамилия from Покупка", db.getConnection);
            DataSet dataset = new DataSet("store");
            DataSet dataset1 = new DataSet("Покупатель");
            DataSet dataset2 = new DataSet("Покупатель");
            sqlDataAdapter.Fill(dataset, "Покупка");
            sqlDataAdapter.Fill(dataset1, "Покупатель");
            sqlDataAdapter.Fill(dataset2, "Покупатель");
            DataTable table;
            DataTable table1;
            DataTable table2;
            table = dataset.Tables["Покупка"];
            table1 = dataset1.Tables["Покупатель"];
            table2 = dataset2.Tables["Покупатель"];
            DataRow row;
            DataRow row1;
            DataRow row2;
            row1 = table1.Rows[0];
            row2 = table2.Rows[0];
            row = table.Rows[0];
            label7.Text = row1["Имя"].ToString();
            label2.Text = row["Баланс"].ToString();
            label19.Text = row2["Фамилия"].ToString();
            db.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
                (string.IsNullOrWhiteSpace(maskedTextBox2.Text) ||
                (string.IsNullOrWhiteSpace(maskedTextBox3.Text) ||
                (string.IsNullOrWhiteSpace(maskedTextBox4.Text)))))

                {
                MessageBox.Show("Введите данные банковской карты", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                decimal balance = Convert.ToInt32(label2.Text);
                balance += Convert.ToInt32(textBox1.Text);
                label2.Text = balance.ToString();
                textBox1.Clear();


                db.OpenConnection();
                string update = "UPDATE Покупка SET Баланс = " + balance.ToString() + "WHERE idПокупка = 3";
                SqlCommand command = new SqlCommand(update, db.getConnection);
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Успешное пополнение баланса", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            main main = new main();
            main.Show();
            this.Hide();
        }
        private void bilet_Load(object sender, EventArgs e)
        {
             
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            decimal ticket1price = 3000.0m;
            decimal balance;
            if (decimal.TryParse(label2.Text, out balance))
                if (balance < ticket1price)
            {
                MessageBox.Show("Недостаточно средств на балансе для покупки билета.", "Ошибка покупки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                balance -= ticket1price;
                label2.Text = balance.ToString("C");

                db.OpenConnection();
                string update = "UPDATE Покупка SET Баланс = @Balance WHERE idПокупка = 3";
                    string insert = "update Регистрация set Билет  = 1 where idРегистрация = 7";
                SqlCommand command = new SqlCommand(update, db.getConnection);
                    SqlCommand command1 = new SqlCommand(insert, db.getConnection);
                    command.Parameters.AddWithValue("@Balance", balance);
                    command.ExecuteNonQuery();
                    command1.ExecuteNonQuery();
                    db.CloseConnection();
                MessageBox.Show("Покупка билета прошла успешно. Ваш новый баланс: " + balance.ToString("C"), "Успешная покупка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Random rand = new Random();
                    int ticketNumber = rand.Next(100, 1000);
                    Aspose.Pdf.Document document = new Aspose.Pdf.Document();
                    Aspose.Pdf.Page page = document.Pages.Add();
                    Aspose.Pdf.Text.TextFragment header = new Aspose.Pdf.Text.TextFragment("БИЛЕТ НА ФЕСТИВАЛЬ");
                    header.TextState.FontSize = 18;
                    header.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
                    header.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                    page.Paragraphs.Add(header);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));


                    Aspose.Pdf.Text.TextFragment ticketNumberFragment = new Aspose.Pdf.Text.TextFragment($"Билет номер: {ticketNumber}");
                    ticketNumberFragment.TextState.FontSize = 12;
                    page.Paragraphs.Add(ticketNumberFragment);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment description = new Aspose.Pdf.Text.TextFragment($"Билет на 1 день (может использоваться на посещение 1 из 3 любых дней мероприятия)");
                    description.TextState.FontSize = 12;
                    page.Paragraphs.Add(description);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment ticketInfo = new Aspose.Pdf.Text.TextFragment($"Цена билета: {ticket1price}");
                    ticketInfo.TextState.FontSize = 12;
                    page.Paragraphs.Add(ticketInfo);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment balanceInfo = new Aspose.Pdf.Text.TextFragment($"Оставшийся баланс: {balance}");
                    balanceInfo.TextState.FontSize = 12;
                    page.Paragraphs.Add(balanceInfo);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment wish = new Aspose.Pdf.Text.TextFragment("Желаем удачно отдохнуть на фестевале!");
                    wish.TextState.FontSize = 12;
                    page.Paragraphs.Add(wish);/// pdf

                    document.Save("C://Users//deraf//source//repos//fest//fest//result//" + "ticket.pdf");

                    MessageBox.Show("Успешно!Чек сохранен и готов к печати", "Перевод в PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            decimal ticket2price = 5000.0m;
            decimal balance;
            if (decimal.TryParse(label2.Text, out balance))
                if (balance < ticket2price)
                {
                    MessageBox.Show("Недостаточно средств на балансе для покупки билета.", "Ошибка покупки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    balance -= ticket2price;
                    label2.Text = balance.ToString("C");

                    db.OpenConnection();
                    string update = "UPDATE Покупка SET Баланс = @Balance WHERE idПокупка = 3";
                    string insert = "update Регистрация set Билет  = 2 where idРегистрация = 7";
                    SqlCommand command = new SqlCommand(update, db.getConnection);
                    SqlCommand command1 = new SqlCommand(insert, db.getConnection);
                    command.Parameters.AddWithValue("@Balance", balance);
                    command.ExecuteNonQuery();
                    command1.ExecuteNonQuery();
                    db.CloseConnection();
                    MessageBox.Show("Покупка билета прошла успешно. Ваш новый баланс: " + balance.ToString("C"), "Успешная покупка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Random rand = new Random();
                    int ticketNumber = rand.Next(100, 1000);
                    Aspose.Pdf.Document document = new Aspose.Pdf.Document();
                    Aspose.Pdf.Page page = document.Pages.Add();
                    Aspose.Pdf.Text.TextFragment header = new Aspose.Pdf.Text.TextFragment("БИЛЕТ НА ФЕСТИВАЛЬ");
                    header.TextState.FontSize = 18;
                    header.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
                    header.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                    page.Paragraphs.Add(header);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));


                    Aspose.Pdf.Text.TextFragment ticketNumberFragment = new Aspose.Pdf.Text.TextFragment($"Билет номер: {ticketNumber}");
                    ticketNumberFragment.TextState.FontSize = 12;
                    page.Paragraphs.Add(ticketNumberFragment);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment description = new Aspose.Pdf.Text.TextFragment($"Билет на 2 дня (может использоваться на посещение 2 из 3 любых дней мероприятия)");
                    description.TextState.FontSize = 12;
                    page.Paragraphs.Add(description);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment ticketInfo = new Aspose.Pdf.Text.TextFragment($"Цена билета: {ticket2price}");
                    ticketInfo.TextState.FontSize = 12;
                    page.Paragraphs.Add(ticketInfo);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment balanceInfo = new Aspose.Pdf.Text.TextFragment($"Оставшийся баланс: {balance}");
                    balanceInfo.TextState.FontSize = 12;
                    page.Paragraphs.Add(balanceInfo);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment wish = new Aspose.Pdf.Text.TextFragment("Желаем удачно отдохнуть на фестевале!");
                    wish.TextState.FontSize = 12;
                    page.Paragraphs.Add(wish);/// pdf

                    document.Save("C://Users//deraf//source//repos//fest//fest//result//" + "ticket.pdf");

                    MessageBox.Show("Успешно!Чек сохранен и готов к печати", "Перевод в PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            decimal ticket3price = 7500.0m;
            decimal balance;
            if (decimal.TryParse(label2.Text, out balance))
                if (balance < ticket3price)
                {
                    MessageBox.Show("Недостаточно средств на балансе для покупки билета.", "Ошибка покупки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    balance -= ticket3price;
                    label2.Text = balance.ToString("C");

                    db.OpenConnection();
                    string update = "UPDATE Покупка SET Баланс = @Balance WHERE idПокупка = 3";
                    string insert = "update Регистрация set Билет  = 3 where idРегистрация = 7";
                    SqlCommand command = new SqlCommand(update, db.getConnection);
                    SqlCommand command1 = new SqlCommand(insert, db.getConnection);
                    command.Parameters.AddWithValue("@Balance", balance);
                    command.ExecuteNonQuery();
                    command1.ExecuteNonQuery();
                    db.CloseConnection();
                    MessageBox.Show("Покупка билета прошла успешно. Ваш новый баланс: " + balance.ToString("C"), "Успешная покупка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Random rand = new Random();
                    int ticketNumber = rand.Next(100, 1000);
                    Aspose.Pdf.Document document = new Aspose.Pdf.Document();
                    Aspose.Pdf.Page page = document.Pages.Add();
                    Aspose.Pdf.Text.TextFragment header = new Aspose.Pdf.Text.TextFragment("БИЛЕТ НА ФЕСТИВАЛЬ");
                    header.TextState.FontSize = 18;
                    header.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
                    header.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                    page.Paragraphs.Add(header);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));


                    Aspose.Pdf.Text.TextFragment ticketNumberFragment = new Aspose.Pdf.Text.TextFragment($"Билет номер: {ticketNumber}");
                    ticketNumberFragment.TextState.FontSize = 12;
                    page.Paragraphs.Add(ticketNumberFragment);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment description = new Aspose.Pdf.Text.TextFragment($"Билет на все 3 дня (может использоваться на посещение всех дней мероприятия)");
                    description.TextState.FontSize = 12;
                    page.Paragraphs.Add(description);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment ticketInfo = new Aspose.Pdf.Text.TextFragment($"Цена билета: {ticket3price}");
                    ticketInfo.TextState.FontSize = 12;
                    page.Paragraphs.Add(ticketInfo);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment balanceInfo = new Aspose.Pdf.Text.TextFragment($"Оставшийся баланс: {balance}");
                    balanceInfo.TextState.FontSize = 12;
                    page.Paragraphs.Add(balanceInfo);
                    page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Environment.NewLine));

                    Aspose.Pdf.Text.TextFragment wish = new Aspose.Pdf.Text.TextFragment("Желаем удачно отдохнуть на фестевале!");
                    wish.TextState.FontSize = 12;
                    page.Paragraphs.Add(wish);/// pdf

                    document.Save("C://Users//deraf//source//repos//fest//fest//result//" + "ticket.pdf");

                    MessageBox.Show("Успешно!Чек сохранен и готов к печати", "Перевод в PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }
    }
}
