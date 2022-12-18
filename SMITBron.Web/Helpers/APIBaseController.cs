using Microsoft.AspNetCore.Mvc;
using NSwag;
using Paramore.Brighter;
using Paramore.Darker;
using SMITBron.Web.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SMITBron.Web.Helpers
{
    public abstract class APIBaseController : ControllerBase
    {
        protected readonly IAmACommandProcessor _commandProcessor;

        protected readonly IQueryProcessor _queryProcessor;

        public const string TimeTakenHeaderKey = "X-Request-Timetaken";

        public APIBaseController(IAmACommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        protected async Task<ActionResult<TResult>> SendCommandAsync<T, TResult>(T command,
            Expression<Func<T, TResult>> resultSelector, int? failureCode = null) where T : class, IRequest
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                
                await _commandProcessor.SendAsync(command);
                
                stopWatch.Stop();

                Response.Headers.Add(TimeTakenHeaderKey, stopWatch.ElapsedMilliseconds.ToString());
                
                return resultSelector != null ? Ok(resultSelector.Compile()(command)) : NoContent();
            }
            catch (FluentValidation.ValidationException ex)
            {
                base.Request.HttpContext.Response.ContentType = "application/json";
                var errors = ex.Errors.Select(x => new ErrorMessageModel
                {
                    Field = x.PropertyName,
                    Error = x.ErrorMessage
                }).ToList();

                return StatusCode(failureCode ?? 500, errors);

            }
            catch (Exception)
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

            Response.Headers.Add(TimeTakenHeaderKey, sw.ElapsedMilliseconds.ToString());

            return Ok(result);
        }

    }
}
