import * as React from "react";
import { Demo } from "../Demo";
import { JsonResult } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <JsonResult id="json-results" />;

export const AutoMapIndex2Demo = () => <Demo
    paramDefinitions={[
        { inputType: "text", name: "country", placeholder: "UK", paramKind: "text-param" }
    ]}
    resultsComponents={resultsCreator}
/>;
