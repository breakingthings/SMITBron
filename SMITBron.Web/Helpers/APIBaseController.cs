using Microsoft.AspNetCore.Mvc;
using NSwag;
using Paramore.Brighter;
using Paramore.Darker;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SMITBron.Web.Helpers
{
    public abstract class APIBaseController : ControllerBase
    {
        protected readonly IAmACommandProcessor _commandProcessor;
        
        protected readonly IQueryProcessor _queryProcessor;

        private const string TimeTakenHeaderKey = "X-Http-Timetaken";

        public APIBaseController(IAmACommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        protected async Task<ActionResult> SendCommandAsync<T>(T command) where T : class, IRequest
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                await _commandProcessor.SendAsync(command);
                sw.Stop();
                HttpContext.Response.Headers.Add(TimeTakenHeaderKey, sw.ElapsedMilliseconds.ToString());
                return Ok();
            } catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Message);
            } catch (Exception)
            {
                throw;
            }
            
        }

        protected async Task<ActionResult<TResult>> DoQueryAsync<TResult>(IQuery<TResult> query)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = await _queryProcessor.ExecuteAsync(query);
            sw.Stop();
            HttpContext.Response.Headers.Add(TimeTakenHeaderKey, sw.ElapsedMilliseconds.ToString());

            return Ok(result);
        }

    }
}
