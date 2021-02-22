using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Models
{
    public class Proprietario
    {
        public int Id                                   { get; set; }
        public TipoConta TipoConta                      { get; set; }
        

        public string Nome                              { get; set; }
        public DateTime DataNascimento                  { get; set; }
        public Byte[] FotoPerfil                        { get; set; }
        public string Email                             { get; set; }
        public byte[] PasswordHash                      { get; set; }
        public byte[] PasswordSalt                      { get; set; }
        
        [NotMapped]
        public string PasswordString                    { get; set; }
        
        public List<Cao> Caes                           { get; set; }
    }
}