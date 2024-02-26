using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;

#region PROCEDIMIENTO ALMACENADO

//create or replace procedure SP_ALL_EMPLEADOS
//(p_cursor_empleados out sys_refcursor)
//as
//begin
//  open p_cursor_empleados for
//	select * from v_empleados;
//end;

//create view v_empleados
//as
//	select isnull(EMP.EMP_NO, 0) as EMP_NO,
//    EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO
//	, DEPT.DNOMBRE, DEPT.LOC, DEPT.DEPT_NO
//	from EMP
//	inner join DEPT
//	on EMP.DEPT_NO=DEPT.DEPT_NO
//go


#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleados : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string sql = "SP_ALL_EMPLEADOS";
            var consulta = this.context.Empleados
                .FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            string sql = "SP_DETAILS_EMPLEADO @idempleado";
            SqlParameter pamId = new SqlParameter("@idempleado", idEmpleado);
            var consulta = this.context.Empleados
                .FromSqlRaw(sql, pamId);
            Empleado empleado = consulta.AsEnumerable().FirstOrDefault();
            return empleado;
        }


    }
}
