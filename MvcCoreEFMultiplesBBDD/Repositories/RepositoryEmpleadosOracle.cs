
#region VIEWS PROCEDURES
//create or replace procedure SP_ALL_EMPLEADOS
//(p_cursor_empleados out sys_refcursor)
//as
//begin
//  open p_cursor_empleados for
//  select * from v_empleados;
//end;

//create or replace procedure SP_DETAILS_EMPLEADO
//(p_cursor_empleados out sys_refcursor,
//p_idempleado EMP.Emp_No%TYPE)
//as
//begin
//  open p_cursor_empleados for
//  select * from v_empleados
//  where IDEMPLEADO=p_idempleado;
//end;

#endregion

using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string sql = "begin ";
            sql += " SP_ALL_EMPLEADOS(:p_cursor_empleados);";
            sql += " end;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_empleados";
            pamCursor.Value = null;
            pamCursor.Direction = ParameterDirection.Output;
            //COMO ES UN TIPO DE ORACLE PROPIO (Cursor) DEBEMOS
            //PONERLO DE FORMA MANUAL
            pamCursor.OracleDbType = OracleDbType.RefCursor;
            var consulta = this.context.Empleados
                .FromSqlRaw(sql, pamCursor);
            return await consulta.ToListAsync();
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
	    {
	        string sql = "begin ";
	        sql += " SP_DETAILS_EMPLEADO (:p_cursor_empleados, :p_idempleado);";
	        sql += " end;";
	        OracleParameter pamCursor = new OracleParameter();
	        pamCursor.ParameterName = "p_cursor_empleados";
	        pamCursor.Value = null;
	        pamCursor.Direction = ParameterDirection.Output;
	        pamCursor.OracleDbType = OracleDbType.RefCursor;
	        OracleParameter pamId = new OracleParameter("p_idempleado", idEmpleado);
	        var consulta = this.context.Empleados.FromSqlRaw(sql, pamCursor, pamId);
	        Empleado empleado = consulta.AsEnumerable().FirstOrDefault();
	        return empleado;
        }
}
}
