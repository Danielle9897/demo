import * as React from "react";
import { Demo } from "../Demo";
import { JsonResult } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <JsonResult id="replication-json-results" />;

export const ReplicationFailoverDemo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "machineName", placeholder: null, paramKind: "text-param" },
        { inputType: "text", name: "id", placeholder: "Users/1", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
