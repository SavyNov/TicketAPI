namespace TicketAPI.Utils {
    public class OperationResult<T> {
        public bool IsSuccesful { get; set; }
        public T? Entity { get; set; }
        public string? Message { get; set; }

        public static OperationResult<T> Success(T? entity, string message) {
            return new OperationResult<T> {
                IsSuccesful=true,
                Entity=entity,
                Message=message
            };
        }

        public static OperationResult<T> Success(string message) {
            return new OperationResult<T> {
                IsSuccesful=true,
                Entity=default,
                Message=message
            };
        }


        public static OperationResult<T> Failure (string message) {
            return new OperationResult<T> {
                IsSuccesful=false,
                Entity=default,
                Message=message
            };
        }

        public static OperationResult<T> Failure (T? entity, string message) {
            return new OperationResult<T> {
                IsSuccesful=false,
                Entity=default,
                Message=message
            };
        }
    }

}
