import React from "react";
export default function Test({ data }) {
  return (
    <div style={{ flexDirection: "row", justifyContent: "space-evenly" }}>
      <p>
        <span>
          <b>TestId</b> : {data.id}
        </span>
        <span>
          <b>Location </b>: {data.testLocation}
        </span>
        <span>
          <b>Type</b> : {data.testType} Test
        </span>
        <span>
          <b>Result</b> : {data.positive ? "Positive" : "Negative"}
        </span>
        <span>
          <b>Booking Number</b> : {data.bookingId}
        </span>
        <span>
          <b>Test Date</b> : {new Date(data.dateCreated).toDateString()}
        </span>

        <span>
          <b>Result Date</b>: {new Date(data.dateUpdated).toDateString()}
        </span>
      </p>
    </div>
  );
}
