﻿// <auto-generated />
using System;
using GoDonate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoDonate.Migrations
{
    [DbContext(typeof(GoDonateDbContext))]
    partial class GoDonateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoDonate.Modul.Autentifikacija.AutentifikacijaToken", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("ipAdresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("korisnickinalogID")
                        .HasColumnType("int");

                    b.Property<string>("vrijednost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("vrijemeEvidencije")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("korisnickinalogID");

                    b.ToTable("AutentifikacijaToken");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Donacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("KolicinaNovca")
                        .HasColumnType("int");

                    b.Property<int>("karticaID")
                        .HasColumnType("int");

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.Property<int>("pricaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("karticaID");

                    b.HasIndex("korisnikID");

                    b.HasIndex("pricaID");

                    b.ToTable("Donacije");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Drzava", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NazivDrzave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skracenica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("valutaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("valutaID");

                    b.ToTable("Drzave");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Grad", b =>
                {
                    b.Property<int>("GradID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradID"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostanskiBroj")
                        .HasColumnType("int");

                    b.Property<int>("drzavaID")
                        .HasColumnType("int");

                    b.HasKey("GradID");

                    b.HasIndex("drzavaID");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Jezik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("korisnikID");

                    b.ToTable("Jezici");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Kartica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrojKartice")
                        .HasColumnType("int");

                    b.Property<int>("CVV_CVC")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DatumVazenja")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GodinaIsteka")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int?>("MjesecIsteka")
                        .HasColumnType("int");

                    b.Property<string>("TipKartice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikID");

                    b.ToTable("Kartice");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Kategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Komentar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("brojDislajkova")
                        .HasColumnType("int");

                    b.Property<int>("brojLajkova")
                        .HasColumnType("int");

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.Property<int>("pricaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("korisnikID");

                    b.HasIndex("pricaID");

                    b.ToTable("Komentari");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.KorisnickiNalog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("SlikaKorisnika")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("KorisnickiNalozi");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Obavijest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DatumObavjestenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipObavijesti")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("korisnikID");

                    b.ToTable("Obavijesti");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Poruka", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("korisnikID");

                    b.ToTable("Poruke");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Prica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Lokacija")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NovcaniCilj")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Slika")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("kategorijaID")
                        .HasColumnType("int");

                    b.Property<int>("korisnikID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("kategorijaID");

                    b.HasIndex("korisnikID");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Valuta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skracenica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Valute");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Administrator", b =>
                {
                    b.HasBaseType("GoDonate.Modul.Models.KorisnickiNalog");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Administratori");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Korisnik", b =>
                {
                    b.HasBaseType("GoDonate.Modul.Models.KorisnickiNalog");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gradID")
                        .HasColumnType("int");

                    b.Property<int>("valutaID")
                        .HasColumnType("int");

                    b.HasIndex("gradID");

                    b.HasIndex("valutaID");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("GoDonate.Modul.Autentifikacija.AutentifikacijaToken", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.KorisnickiNalog", "korisnickinalog")
                        .WithMany()
                        .HasForeignKey("korisnickinalogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("korisnickinalog");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Donacija", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Kartica", "Kartica")
                        .WithMany()
                        .HasForeignKey("karticaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoDonate.Modul.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoDonate.Modul.Models.Prica", "Prica")
                        .WithMany()
                        .HasForeignKey("pricaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kartica");

                    b.Navigation("Korisnik");

                    b.Navigation("Prica");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Drzava", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Valuta", "Valuta")
                        .WithMany()
                        .HasForeignKey("valutaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Valuta");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Grad", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Drzava", "Drzava")
                        .WithMany()
                        .HasForeignKey("drzavaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drzava");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Jezik", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Kartica", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Komentar", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoDonate.Modul.Models.Prica", "Prica")
                        .WithMany()
                        .HasForeignKey("pricaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Prica");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Obavijest", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Poruka", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Prica", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.Kategorija", "Kategorija")
                        .WithMany()
                        .HasForeignKey("kategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoDonate.Modul.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategorija");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Administrator", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("GoDonate.Modul.Models.Administrator", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoDonate.Modul.Models.Korisnik", b =>
                {
                    b.HasOne("GoDonate.Modul.Models.KorisnickiNalog", null)
                        .WithOne()
                        .HasForeignKey("GoDonate.Modul.Models.Korisnik", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoDonate.Modul.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("gradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoDonate.Modul.Models.Valuta", "Valuta")
                        .WithMany()
                        .HasForeignKey("valutaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grad");

                    b.Navigation("Valuta");
                });
#pragma warning restore 612, 618
        }
    }
}
