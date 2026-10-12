using EcoControl.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoControl.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Equipamento> Equipamentos { get; set; }
    public DbSet<LeituraSensor> LeiturasSensor { get; set; }
    public DbSet<LogDesligamento> LogsDesligamento { get; set; }
    public DbSet<ConsumoEnergia> ConsumosEnergia { get; set; }
    public DbSet<HorarioOperacao> HorariosOperacao { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipamento>(e =>
        {
            e.ToTable("EQUIPAMENTO");
            e.HasKey(x => x.IdEquipamento);
            e.Property(x => x.IdEquipamento).HasColumnName("ID_EQUIPAMENTO").UseHiLo("SEQ_EQUIPAMENTO");
            e.Property(x => x.NomeEquipamento).HasColumnName("NOME_EQUIPAMENTO").HasMaxLength(100).IsRequired();
            e.Property(x => x.Setor).HasColumnName("SETOR").HasMaxLength(80).IsRequired();
            e.Property(x => x.PotenciaKw).HasColumnName("POTENCIA_KW").HasPrecision(10, 2).IsRequired();
            e.Property(x => x.StatusAtual).HasColumnName("STATUS_ATUAL").HasMaxLength(20).HasDefaultValue("LIGADO");
            e.Property(x => x.DtCadastro).HasColumnName("DT_CADASTRO").HasDefaultValueSql("SYSDATE");
        });

        modelBuilder.Entity<LeituraSensor>(e =>
        {
            e.ToTable("LEITURA_SENSOR");
            e.HasKey(x => x.IdLeitura);
            e.Property(x => x.IdLeitura).HasColumnName("ID_LEITURA").UseHiLo("SEQ_LEITURA_SENSOR");
            e.Property(x => x.IdEquipamento).HasColumnName("ID_EQUIPAMENTO").IsRequired();
            e.Property(x => x.DtLeitura).HasColumnName("DT_LEITURA").HasDefaultValueSql("SYSDATE");
            e.Property(x => x.EquipamentoAtivo).HasColumnName("EQUIPAMENTO_ATIVO").HasMaxLength(1).HasDefaultValue("N");
            e.Property(x => x.ConsumoInstKw).HasColumnName("CONSUMO_INST_KW").HasPrecision(10, 3).HasDefaultValue(0m);
            e.HasOne(x => x.Equipamento).WithMany(x => x.LeiturasSensor).HasForeignKey(x => x.IdEquipamento);
        });

        modelBuilder.Entity<LogDesligamento>(e =>
        {
            e.ToTable("LOG_DESLIGAMENTO");
            e.HasKey(x => x.IdLog);
            e.Property(x => x.IdLog).HasColumnName("ID_LOG").UseHiLo("SEQ_LOG_DESLIG");
            e.Property(x => x.IdEquipamento).HasColumnName("ID_EQUIPAMENTO").IsRequired();
            e.Property(x => x.DtDesligamento).HasColumnName("DT_DESLIGAMENTO").HasDefaultValueSql("SYSDATE");
            e.Property(x => x.Motivo).HasColumnName("MOTIVO").HasMaxLength(200).IsRequired();
            e.Property(x => x.TipoDesligamento).HasColumnName("TIPO_DESLIGAMENTO").HasMaxLength(30).HasDefaultValue("AUTOMATICO");
            e.HasOne(x => x.Equipamento).WithMany(x => x.LogsDesligamento).HasForeignKey(x => x.IdEquipamento);
        });

        modelBuilder.Entity<ConsumoEnergia>(e =>
        {
            e.ToTable("CONSUMO_ENERGIA");
            e.HasKey(x => x.IdConsumo);
            e.Property(x => x.IdConsumo).HasColumnName("ID_CONSUMO").UseHiLo("SEQ_CONSUMO_ENERGIA");
            e.Property(x => x.IdEquipamento).HasColumnName("ID_EQUIPAMENTO").IsRequired();
            e.Property(x => x.DtRegistro).HasColumnName("DT_REGISTRO").HasDefaultValueSql("SYSDATE");
            e.Property(x => x.KwhConsumido).HasColumnName("KWH_CONSUMIDO").HasPrecision(12, 4).HasDefaultValue(0m);
            e.Property(x => x.KwhEconomizado).HasColumnName("KWH_ECONOMIZADO").HasPrecision(12, 4).HasDefaultValue(0m);
            e.Property(x => x.PeriodoHoras).HasColumnName("PERIODO_HORAS").HasPrecision(8, 2).HasDefaultValue(0m);
            e.Property(x => x.CustoEstimadoR).HasColumnName("CUSTO_ESTIMADO_R").HasPrecision(12, 2).HasDefaultValue(0m);
            e.HasOne(x => x.Equipamento).WithMany(x => x.ConsumosEnergia).HasForeignKey(x => x.IdEquipamento);
        });

        modelBuilder.Entity<HorarioOperacao>(e =>
        {
            e.ToTable("HORARIO_OPERACAO");
            e.HasKey(x => x.IdHorario);
            e.Property(x => x.IdHorario).HasColumnName("ID_HORARIO").UseHiLo("SEQ_HORARIO_OP");
            e.Property(x => x.IdEquipamento).HasColumnName("ID_EQUIPAMENTO").IsRequired();
            e.Property(x => x.DiaSemana).HasColumnName("DIA_SEMANA").HasMaxLength(15).IsRequired();
            e.Property(x => x.HoraInicio).HasColumnName("HORA_INICIO").HasPrecision(4, 2).IsRequired();
            e.Property(x => x.HoraFim).HasColumnName("HORA_FIM").HasPrecision(4, 2).IsRequired();
            e.Property(x => x.TempoMaxOcioso).HasColumnName("TEMPO_MAX_OCIOSO").HasDefaultValue(30);
            e.HasOne(x => x.Equipamento).WithMany(x => x.HorariosOperacao).HasForeignKey(x => x.IdEquipamento);
        });
    }
}