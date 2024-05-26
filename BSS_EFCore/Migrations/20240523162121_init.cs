using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSS_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ADMINISTRATOR",
                columns: table => new
                {
                    ADMIN_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ACCOUNT_SERIAL = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PASSWORD = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009148", x => x.ADMIN_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BATTERY_TYPE",
                columns: table => new
                {
                    BATTERY_TYPE_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MAX_CHARGE_TIEMS = table.Column<int>(type: "int", nullable: false),
                    TOTAL_CAPACITY = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009070", x => x.BATTERY_TYPE_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SWITCH_STATION",
                columns: table => new
                {
                    STATION_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    STATION_NAME = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SERVICE_FEE = table.Column<float>(type: "float", nullable: false),
                    ElectricityFee = table.Column<float>(type: "float", nullable: false),
                    ParkingFee = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QueueLength = table.Column<int>(type: "int", nullable: false),
                    LONGITUDE = table.Column<double>(type: "double", nullable: false),
                    LATITUDE = table.Column<double>(type: "double", nullable: false),
                    FAILURE_STATUS = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BATTERY_CAPACITY = table.Column<int>(type: "int", nullable: false),
                    AVAILABLE_BATTERY_COUNT = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tags = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeSpan = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009065", x => x.STATION_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VEHICLE_OWNER",
                columns: table => new
                {
                    OWNER_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ACCOUNT_SERIAL = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USERNAME = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EMAIL = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PASSWORD = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PROFILE_PHOTO = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CREATE_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GENDER = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BIRTHDAY = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009088", x => x.OWNER_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VEHICLE_PARAM",
                columns: table => new
                {
                    VEHICLE_MODEL = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModelName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRANSMISSION = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SERVICE_TERM = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MANUFACTURER = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MAX_SPEED = table.Column<int>(type: "int", nullable: false),
                    SINP = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009081", x => x.VEHICLE_MODEL);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NEWS",
                columns: table => new
                {
                    ANNOUNCEMENT_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PUBLISH_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PUBLISH_POS = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    TITLE = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CONTENTS = table.Column<string>(type: "varchar(2500)", maxLength: 2500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LIKES = table.Column<int>(type: "int", nullable: false),
                    VIEW_COUNT = table.Column<int>(type: "int", nullable: false),
                    administratorAdminId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009098", x => x.ANNOUNCEMENT_ID);
                    table.ForeignKey(
                        name: "FK_NEWS_ADMINISTRATOR_administratorAdminId",
                        column: x => x.administratorAdminId,
                        principalTable: "ADMINISTRATOR",
                        principalColumn: "ADMIN_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BATTERY",
                columns: table => new
                {
                    BATTERY_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AVAILABLE_STATUS = table.Column<int>(type: "int", nullable: true),
                    CURRENT_CAPACITY = table.Column<double>(type: "double", nullable: false),
                    CURR_CHARGE_TIMES = table.Column<int>(type: "int", nullable: false),
                    MANUFACTURING_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    switchStationStationId = table.Column<long>(type: "bigint", nullable: true),
                    BatteryTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009076", x => x.BATTERY_ID);
                    table.ForeignKey(
                        name: "FK_BATTERY_BATTERY_TYPE_BatteryTypeId",
                        column: x => x.BatteryTypeId,
                        principalTable: "BATTERY_TYPE",
                        principalColumn: "BATTERY_TYPE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BATTERY_SWITCH_STATION_switchStationStationId",
                        column: x => x.switchStationStationId,
                        principalTable: "SWITCH_STATION",
                        principalColumn: "STATION_ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    EMPLOYEE_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ACCOUNT_SERIAL = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USERNAME = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PASSWORD = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "123456")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PROFILE_PHOTO = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CREATE_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IDENTITYNUMBER = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NAME = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GENDER = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    POSITIONS = table.Column<int>(type: "int", nullable: false),
                    SALARY = table.Column<int>(type: "int", nullable: false),
                    switchStationStationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009095", x => x.EMPLOYEE_ID);
                    table.ForeignKey(
                        name: "FK_EMPLOYEE_SWITCH_STATION_switchStationStationId",
                        column: x => x.switchStationStationId,
                        principalTable: "SWITCH_STATION",
                        principalColumn: "STATION_ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OWNERPOS",
                columns: table => new
                {
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    ADDRESS = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009099", x => x.OwnerId);
                    table.ForeignKey(
                        name: "FK_OWNERPOS_VEHICLE_OWNER_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "VEHICLE_OWNER",
                        principalColumn: "OWNER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VEHICLE",
                columns: table => new
                {
                    VEHICLE_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PURCHASE_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BatteryId = table.Column<long>(type: "bigint", nullable: false),
                    PLATE_NUMBER = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    Warranty = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    vehicleOwnerOwnerId = table.Column<long>(type: "bigint", nullable: false),
                    vehicleParamVehicleModelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009110", x => x.VEHICLE_ID);
                    table.ForeignKey(
                        name: "FK_VEHICLE_BATTERY_BatteryId",
                        column: x => x.BatteryId,
                        principalTable: "BATTERY",
                        principalColumn: "BATTERY_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VEHICLE_VEHICLE_OWNER_vehicleOwnerOwnerId",
                        column: x => x.vehicleOwnerOwnerId,
                        principalTable: "VEHICLE_OWNER",
                        principalColumn: "OWNER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VEHICLE_VEHICLE_PARAM_vehicleParamVehicleModelId",
                        column: x => x.vehicleParamVehicleModelId,
                        principalTable: "VEHICLE_PARAM",
                        principalColumn: "VEHICLE_MODEL",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KPI",
                columns: table => new
                {
                    KPI_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TOTAL_PERFORMANCE = table.Column<double>(type: "double", nullable: false),
                    SERVICE_FREQUENCY = table.Column<int>(type: "int", nullable: false),
                    SCORE = table.Column<double>(type: "double", nullable: false),
                    employeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009105", x => x.KPI_ID);
                    table.ForeignKey(
                        name: "FK_KPI_EMPLOYEE_employeeId",
                        column: x => x.employeeId,
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MAINTENANCE_ITEM",
                columns: table => new
                {
                    MAINTENANCE_ITEM_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MAINTENANCE_LOCATION = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NOTE = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TITLE = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    longitude = table.Column<double>(type: "double", nullable: false),
                    latitude = table.Column<double>(type: "double", nullable: false),
                    SERVICE_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ORDER_SUBMISSION_TIME = table.Column<DateTime>(type: "datetime(6)", precision: 6, nullable: false),
                    APPOINT_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ORDER_STATUS = table.Column<int>(type: "int", nullable: false),
                    SCORE = table.Column<double>(type: "double", nullable: false),
                    Evaluation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VehicleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009117", x => x.MAINTENANCE_ITEM_ID);
                    table.ForeignKey(
                        name: "FK_MAINTENANCE_ITEM_VEHICLE_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "VEHICLE",
                        principalColumn: "VEHICLE_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SWITCH_REQUEST",
                columns: table => new
                {
                    SWITCH_REQUEST_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SWITCH_TYPE = table.Column<int>(type: "int", nullable: false),
                    REQUEST_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    POSITION = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    NOTES = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PERIOD = table.Column<int>(type: "int", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    VehicleId = table.Column<long>(type: "bigint", nullable: false),
                    BatteryTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008772", x => x.SWITCH_REQUEST_ID);
                    table.ForeignKey(
                        name: "FK_SWITCH_REQUEST_BATTERY_TYPE_BatteryTypeId",
                        column: x => x.BatteryTypeId,
                        principalTable: "BATTERY_TYPE",
                        principalColumn: "BATTERY_TYPE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SWITCH_REQUEST_EMPLOYEE_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SWITCH_REQUEST_VEHICLE_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "VEHICLE",
                        principalColumn: "VEHICLE_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employee_MaintenanceItem",
                columns: table => new
                {
                    employeesEmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    maintenanceItemsMaintenanceItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_MaintenanceItem", x => new { x.employeesEmployeeId, x.maintenanceItemsMaintenanceItemId });
                    table.ForeignKey(
                        name: "FK_Employee_MaintenanceItem_EMPLOYEE_employeesEmployeeId",
                        column: x => x.employeesEmployeeId,
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_MaintenanceItem_MAINTENANCE_ITEM_maintenanceItemsMa~",
                        column: x => x.maintenanceItemsMaintenanceItemId,
                        principalTable: "MAINTENANCE_ITEM",
                        principalColumn: "MAINTENANCE_ITEM_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SWITCH_LOG",
                columns: table => new
                {
                    SWITCH_SERVICE_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SWITCH_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SCORE = table.Column<double>(type: "double", nullable: false),
                    Evaluation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServiceFee = table.Column<float>(type: "float", nullable: false),
                    batteryOnBatteryId = table.Column<long>(type: "bigint", nullable: false),
                    batteryOffBatteryId = table.Column<long>(type: "bigint", nullable: false),
                    switchRequestId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C009138", x => x.SWITCH_SERVICE_ID);
                    table.ForeignKey(
                        name: "FK_SWITCH_LOG_BATTERY_batteryOffBatteryId",
                        column: x => x.batteryOffBatteryId,
                        principalTable: "BATTERY",
                        principalColumn: "BATTERY_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SWITCH_LOG_BATTERY_batteryOnBatteryId",
                        column: x => x.batteryOnBatteryId,
                        principalTable: "BATTERY",
                        principalColumn: "BATTERY_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SWITCH_LOG_SWITCH_REQUEST_switchRequestId",
                        column: x => x.switchRequestId,
                        principalTable: "SWITCH_REQUEST",
                        principalColumn: "SWITCH_REQUEST_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BATTERY_BatteryTypeId",
                table: "BATTERY",
                column: "BatteryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BATTERY_switchStationStationId",
                table: "BATTERY",
                column: "switchStationStationId");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_switchStationStationId",
                table: "EMPLOYEE",
                column: "switchStationStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_MaintenanceItem_maintenanceItemsMaintenanceItemId",
                table: "Employee_MaintenanceItem",
                column: "maintenanceItemsMaintenanceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_employeeId",
                table: "KPI",
                column: "employeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MAINTENANCE_ITEM_VehicleId",
                table: "MAINTENANCE_ITEM",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_NEWS_administratorAdminId",
                table: "NEWS",
                column: "administratorAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SWITCH_LOG_batteryOffBatteryId",
                table: "SWITCH_LOG",
                column: "batteryOffBatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_SWITCH_LOG_batteryOnBatteryId",
                table: "SWITCH_LOG",
                column: "batteryOnBatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_SWITCH_LOG_switchRequestId",
                table: "SWITCH_LOG",
                column: "switchRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SWITCH_REQUEST_BatteryTypeId",
                table: "SWITCH_REQUEST",
                column: "BatteryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SWITCH_REQUEST_EmployeeId",
                table: "SWITCH_REQUEST",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SWITCH_REQUEST_VehicleId",
                table: "SWITCH_REQUEST",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_BatteryId",
                table: "VEHICLE",
                column: "BatteryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_vehicleOwnerOwnerId",
                table: "VEHICLE",
                column: "vehicleOwnerOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_vehicleParamVehicleModelId",
                table: "VEHICLE",
                column: "vehicleParamVehicleModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee_MaintenanceItem");

            migrationBuilder.DropTable(
                name: "KPI");

            migrationBuilder.DropTable(
                name: "NEWS");

            migrationBuilder.DropTable(
                name: "OWNERPOS");

            migrationBuilder.DropTable(
                name: "SWITCH_LOG");

            migrationBuilder.DropTable(
                name: "MAINTENANCE_ITEM");

            migrationBuilder.DropTable(
                name: "ADMINISTRATOR");

            migrationBuilder.DropTable(
                name: "SWITCH_REQUEST");

            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "VEHICLE");

            migrationBuilder.DropTable(
                name: "BATTERY");

            migrationBuilder.DropTable(
                name: "VEHICLE_OWNER");

            migrationBuilder.DropTable(
                name: "VEHICLE_PARAM");

            migrationBuilder.DropTable(
                name: "BATTERY_TYPE");

            migrationBuilder.DropTable(
                name: "SWITCH_STATION");
        }
    }
}
