using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoControl.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_CONSUMO_ENERGIA",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "SEQ_EQUIPAMENTO",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "SEQ_HORARIO_OP",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "SEQ_LEITURA_SENSOR",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "SEQ_LOG_DESLIG",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "EQUIPAMENTO",
                columns: table => new
                {
                    ID_EQUIPAMENTO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    NOME_EQUIPAMENTO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    SETOR = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    POTENCIA_KW = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    STATUS_ATUAL = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, defaultValue: "LIGADO"),
                    DT_CADASTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSDATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EQUIPAMENTO", x => x.ID_EQUIPAMENTO);
                });

            migrationBuilder.CreateTable(
                name: "CONSUMO_ENERGIA",
                columns: table => new
                {
                    ID_CONSUMO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_EQUIPAMENTO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DT_REGISTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSDATE"),
                    KWH_CONSUMIDO = table.Column<decimal>(type: "DECIMAL(12,4)", precision: 12, scale: 4, nullable: false, defaultValue: 0m),
                    KWH_ECONOMIZADO = table.Column<decimal>(type: "DECIMAL(12,4)", precision: 12, scale: 4, nullable: false, defaultValue: 0m),
                    PERIODO_HORAS = table.Column<decimal>(type: "DECIMAL(8,2)", precision: 8, scale: 2, nullable: false, defaultValue: 0m),
                    CUSTO_ESTIMADO_R = table.Column<decimal>(type: "DECIMAL(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSUMO_ENERGIA", x => x.ID_CONSUMO);
                    table.ForeignKey(
                        name: "FK_CONSUMO_ENERGIA_EQUIPAMENTO_ID_EQUIPAMENTO",
                        column: x => x.ID_EQUIPAMENTO,
                        principalTable: "EQUIPAMENTO",
                        principalColumn: "ID_EQUIPAMENTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HORARIO_OPERACAO",
                columns: table => new
                {
                    ID_HORARIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_EQUIPAMENTO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DIA_SEMANA = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    HORA_INICIO = table.Column<decimal>(type: "DECIMAL(4,2)", precision: 4, scale: 2, nullable: false),
                    HORA_FIM = table.Column<decimal>(type: "DECIMAL(4,2)", precision: 4, scale: 2, nullable: false),
                    TEMPO_MAX_OCIOSO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 30)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HORARIO_OPERACAO", x => x.ID_HORARIO);
                    table.ForeignKey(
                        name: "FK_HORARIO_OPERACAO_EQUIPAMENTO_ID_EQUIPAMENTO",
                        column: x => x.ID_EQUIPAMENTO,
                        principalTable: "EQUIPAMENTO",
                        principalColumn: "ID_EQUIPAMENTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LEITURA_SENSOR",
                columns: table => new
                {
                    ID_LEITURA = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_EQUIPAMENTO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DT_LEITURA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSDATE"),
                    EQUIPAMENTO_ATIVO = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false, defaultValue: "N"),
                    CONSUMO_INST_KW = table.Column<decimal>(type: "DECIMAL(10,3)", precision: 10, scale: 3, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEITURA_SENSOR", x => x.ID_LEITURA);
                    table.ForeignKey(
                        name: "FK_LEITURA_SENSOR_EQUIPAMENTO_ID_EQUIPAMENTO",
                        column: x => x.ID_EQUIPAMENTO,
                        principalTable: "EQUIPAMENTO",
                        principalColumn: "ID_EQUIPAMENTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LOG_DESLIGAMENTO",
                columns: table => new
                {
                    ID_LOG = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_EQUIPAMENTO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DT_DESLIGAMENTO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSDATE"),
                    MOTIVO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    TIPO_DESLIGAMENTO = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false, defaultValue: "AUTOMATICO")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOG_DESLIGAMENTO", x => x.ID_LOG);
                    table.ForeignKey(
                        name: "FK_LOG_DESLIGAMENTO_EQUIPAMENTO_ID_EQUIPAMENTO",
                        column: x => x.ID_EQUIPAMENTO,
                        principalTable: "EQUIPAMENTO",
                        principalColumn: "ID_EQUIPAMENTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONSUMO_ENERGIA_ID_EQUIPAMENTO",
                table: "CONSUMO_ENERGIA",
                column: "ID_EQUIPAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_HORARIO_OPERACAO_ID_EQUIPAMENTO",
                table: "HORARIO_OPERACAO",
                column: "ID_EQUIPAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_LEITURA_SENSOR_ID_EQUIPAMENTO",
                table: "LEITURA_SENSOR",
                column: "ID_EQUIPAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_LOG_DESLIGAMENTO_ID_EQUIPAMENTO",
                table: "LOG_DESLIGAMENTO",
                column: "ID_EQUIPAMENTO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONSUMO_ENERGIA");

            migrationBuilder.DropTable(
                name: "HORARIO_OPERACAO");

            migrationBuilder.DropTable(
                name: "LEITURA_SENSOR");

            migrationBuilder.DropTable(
                name: "LOG_DESLIGAMENTO");

            migrationBuilder.DropTable(
                name: "EQUIPAMENTO");

            migrationBuilder.DropSequence(
                name: "SEQ_CONSUMO_ENERGIA");

            migrationBuilder.DropSequence(
                name: "SEQ_EQUIPAMENTO");

            migrationBuilder.DropSequence(
                name: "SEQ_HORARIO_OP");

            migrationBuilder.DropSequence(
                name: "SEQ_LEITURA_SENSOR");

            migrationBuilder.DropSequence(
                name: "SEQ_LOG_DESLIG");
        }
    }
}
