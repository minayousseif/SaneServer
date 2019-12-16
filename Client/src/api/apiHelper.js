export const handleApiErrors = (error, callback) => {
  let humanReadableError = 'Oops! something went wrong.';
  if (error.response) {
    // The request was made and the server responded with a status code
    // that falls out of the range of 2xx
    const resp = error.response;
    humanReadableError = `${humanReadableError} ${resp.status}: ${resp.statusText}`
  } else if (error.request) {
    // The request was made but no response was received
    // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
    // http.ClientRequest in node.js
    console.log(error.request);
  } else {
    // Something happened in setting up the request that triggered an Error
    humanReadableError = error.message;
  }
  callback(humanReadableError);
};