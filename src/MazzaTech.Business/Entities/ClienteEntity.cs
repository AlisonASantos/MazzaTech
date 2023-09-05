namespace MazzaTech.Business.Models
{
    public class ClienteEntity : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<EnderecoEntity> Enderecos { get; set; }
        public bool Ativo { get; set; }
    }
}