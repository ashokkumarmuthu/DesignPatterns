# Decorator Pattern Challenge: Secure HTTP API Response Middleware Pipeline 🔒

### Scenario
You are building an API Gateway middleware framework. Incoming API responses need optional, stackable processing layers based on endpoint annotations or tenant policies:
1. **Plain Text Handler:** Returns raw JSON payload.
2. **GZip Compression Decorator:** Compresses the JSON payload into GZip base64 bytes for bandwidth savings.
3. **AES Encryption Decorator:** Encrypts the payload for compliance security.
4. **Audit & Execution Timing Decorator:** Measures pipeline execution time in milliseconds and records request metadata.

---

### What it teaches:
* **The Decorator Design Pattern:** Dynamically attaching cross-cutting concerns to an object without subclass explosion.
* **Stackable Behavior Composition:** Combining `Timing(Encryption(Compression(PlainHandler)))` at runtime in any order.

---

### Core Requirements
1. **Component Interface (`IApiResponsePipeline`):**
   - `ProcessResponse(string rawJsonPayload)` -> returns processed output string.
2. **Base Component (`RawJsonPipeline`):** Returns raw payload.
3. **Base Decorator (`PipelineDecorator`):** Implements `IApiResponsePipeline`, wraps inner pipeline.
4. **Concrete Decorators:**
   - `CompressionPipelineDecorator`
   - `EncryptionPipelineDecorator`
   - `TelemetryPipelineDecorator`
