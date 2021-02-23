namespace PetFelizApi.Models
{
    public class CaoSolicitacao
    {
        public int SolicitacaoServicoId     { get; set; }
        public SolicitacaoServico SolicitacaoServico { get; set; }
        public int CaoId        { get; set; }
        public Cao Cao          { get; set; }
    }
}