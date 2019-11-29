using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Latvanyossagok
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=latvanyossagokdb;Uid=root;Pwd=;");
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = @"CREATE TABLE `latvanyossagokdb`.`varosok`
            ( `id` INT NOT NULL AUTO_INCREMENT , `nev` VARCHAR(70) NOT NULL ,
            PRIMARY KEY (`id`), UNIQUE (`nev`)) ENGINE = InnoDB,
            ALTER TABLE `varosok` ADD `lakossag` INT NOT NULL AFTER `nev`;
            ) ";
            command.CommandText = @"CREATE TABLE `latvanyossagokdb`.`latvanyossagok` ( `id` INT NOT NULL AUTO_INCREMENT ,
            `nev` VARCHAR(70) NOT NULL ,
            `leiras` VARCHAR(70) NOT NULL ,  `ar` INT NOT NULL ,
            `varos_id` INT NOT NULL ,    PRIMARY KEY  (`id`)) ENGINE = InnoDB;)";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonkuldes_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO varosok(nev,lakossag) VALUES (@nev, @lakossag)";
                    cmd.Parameters.AddWithValue("@nev", textBox1.Text);
                    cmd.Parameters.AddWithValue("@lakossag", lakossagnumeric.Value);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("nem toltott ki mindent");
                }
               
            }
            catch (MySqlException ex )
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("mar van ilyen nevu varos");
                }
                
            }
            



        }
    }
}
