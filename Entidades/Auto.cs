using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Auto
    {
        protected string color;
        protected string marca;
        protected string modelo;
        protected int kms;
        protected string patente;

        public Auto(string color, string marca, string modelo, int kms, string patente)
        {
            this.color = color;
            this.marca = marca;
            this.modelo = modelo;
            this.kms = kms;
            this.patente = patente;
        }

        public string Color { get => color; }
        public string Marca { get => marca; }
        public string Modelo { get => modelo; }
        public int Kms { get => kms; }
        public string Patente { get => patente; }

        public override string ToString()
        {
            return $"Marca: {this.Marca} – Modelo: {this.Modelo} – Kms: {this.Kms} – Color: {this.Color} – Patente: {this.Patente}";
        }


    }
}
