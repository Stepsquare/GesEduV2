namespace GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro
{
    public class GetFuncionariosUoRespResponse
    {
        public string? dt_exportacao { get; set; }
        public List<Funcionario> funcionarios { get; set; } = [];

        public class Funcionario
        {
            public int id_pessoa_unica { get; set; }
            public string? nif { get; set; }
            public string? nome { get; set; }
            public string? regime { get; set; }
            public string? desc_regime { get; set; }
            public string? categoria { get; set; }
            public string? vinculo { get; set; }
            public string? indice_nivel { get; set; }
            public string? dt_inicio_funcoes { get; set; }
            public string? dt_fim_funcoes { get; set; }
            public List<Doc> docs { get; set; } = [];
            public List<Morada> moradas { get; set; } = [];
            public List<Contato> contatos { get; set; } = [];

            public class Doc
            {
                public string? cod_tipo_documento { get; set; }
                public string? desc_tipo_documento { get; set; }
                public string? tx_num_documento { get; set; }
                public string? dt_validade { get; set; }
            }

            public class Morada
            {
                public string? lov_tipo_morada { get; set; }
                public string? tx_nome_arteria { get; set; }
                public string? lov_cod_postal { get; set; }
                public string? localidade { get; set; }
                public string? lov_pais { get; set; }
                public string? lov_distrito { get; set; }
                public string? lov_concelho { get; set; }
                public string? lov_freguesia { get; set; }
            }

            public class Contato
            {
                public string? cod_tipo_contacto { get; set; }
                public string? desc_tipo_contacto { get; set; }
                public string? tx_contacto { get; set; }
            }
        }
    }
}
