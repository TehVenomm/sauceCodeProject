package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonParser.NumberType;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.node.ArrayNode;
import com.fasterxml.jackson.databind.node.JsonNodeFactory;
import com.fasterxml.jackson.databind.node.ObjectNode;
import com.fasterxml.jackson.databind.util.RawValue;
import java.io.IOException;

/* compiled from: JsonNodeDeserializer */
abstract class BaseNodeDeserializer<T extends JsonNode> extends StdDeserializer<T> {
    public BaseNodeDeserializer(Class<T> cls) {
        super(cls);
    }

    public Object deserializeWithType(JsonParser jsonParser, DeserializationContext deserializationContext, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromAny(jsonParser, deserializationContext);
    }

    public boolean isCachable() {
        return true;
    }

    /* access modifiers changed from: protected */
    public void _reportProblem(JsonParser jsonParser, String str) throws JsonMappingException {
        throw JsonMappingException.from(jsonParser, str);
    }

    /* access modifiers changed from: protected */
    public void _handleDuplicateField(JsonParser jsonParser, DeserializationContext deserializationContext, JsonNodeFactory jsonNodeFactory, String str, ObjectNode objectNode, JsonNode jsonNode, JsonNode jsonNode2) throws JsonProcessingException {
        if (deserializationContext.isEnabled(DeserializationFeature.FAIL_ON_READING_DUP_TREE_KEY)) {
            _reportProblem(jsonParser, "Duplicate field '" + str + "' for ObjectNode: not allowed when FAIL_ON_READING_DUP_TREE_KEY enabled");
        }
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:4:0x0010  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.fasterxml.jackson.databind.node.ObjectNode deserializeObject(com.fasterxml.jackson.core.JsonParser r9, com.fasterxml.jackson.databind.DeserializationContext r10, com.fasterxml.jackson.databind.node.JsonNodeFactory r11) throws java.io.IOException {
        /*
            r8 = this;
            com.fasterxml.jackson.databind.node.ObjectNode r5 = r11.objectNode()
            boolean r0 = r9.isExpectedStartObjectToken()
            if (r0 == 0) goto L_0x0031
            java.lang.String r4 = r9.nextFieldName()
        L_0x000e:
            if (r4 == 0) goto L_0x0039
            com.fasterxml.jackson.core.JsonToken r0 = r9.nextToken()
            int r0 = r0.mo9113id()
            switch(r0) {
                case 1: goto L_0x0050;
                case 2: goto L_0x001b;
                case 3: goto L_0x0055;
                case 4: goto L_0x001b;
                case 5: goto L_0x001b;
                case 6: goto L_0x005f;
                case 7: goto L_0x0068;
                case 8: goto L_0x001b;
                case 9: goto L_0x006d;
                case 10: goto L_0x0073;
                case 11: goto L_0x0079;
                case 12: goto L_0x005a;
                default: goto L_0x001b;
            }
        L_0x001b:
            com.fasterxml.jackson.databind.JsonNode r7 = r8.deserializeAny(r9, r10, r11)
        L_0x001f:
            com.fasterxml.jackson.databind.JsonNode r6 = r5.replace(r4, r7)
            if (r6 == 0) goto L_0x002c
            r0 = r8
            r1 = r9
            r2 = r10
            r3 = r11
            r0._handleDuplicateField(r1, r2, r3, r4, r5, r6, r7)
        L_0x002c:
            java.lang.String r4 = r9.nextFieldName()
            goto L_0x000e
        L_0x0031:
            com.fasterxml.jackson.core.JsonToken r0 = r9.getCurrentToken()
            com.fasterxml.jackson.core.JsonToken r1 = com.fasterxml.jackson.core.JsonToken.END_OBJECT
            if (r0 != r1) goto L_0x003a
        L_0x0039:
            return r5
        L_0x003a:
            com.fasterxml.jackson.core.JsonToken r1 = com.fasterxml.jackson.core.JsonToken.FIELD_NAME
            if (r0 == r1) goto L_0x004b
            java.lang.Class r0 = r8.handledType()
            com.fasterxml.jackson.core.JsonToken r1 = r9.getCurrentToken()
            com.fasterxml.jackson.databind.JsonMappingException r0 = r10.mappingException(r0, r1)
            throw r0
        L_0x004b:
            java.lang.String r4 = r9.getCurrentName()
            goto L_0x000e
        L_0x0050:
            com.fasterxml.jackson.databind.node.ObjectNode r7 = r8.deserializeObject(r9, r10, r11)
            goto L_0x001f
        L_0x0055:
            com.fasterxml.jackson.databind.node.ArrayNode r7 = r8.deserializeArray(r9, r10, r11)
            goto L_0x001f
        L_0x005a:
            com.fasterxml.jackson.databind.JsonNode r7 = r8._fromEmbedded(r9, r10, r11)
            goto L_0x001f
        L_0x005f:
            java.lang.String r0 = r9.getText()
            com.fasterxml.jackson.databind.node.TextNode r7 = r11.textNode(r0)
            goto L_0x001f
        L_0x0068:
            com.fasterxml.jackson.databind.JsonNode r7 = r8._fromInt(r9, r10, r11)
            goto L_0x001f
        L_0x006d:
            r0 = 1
            com.fasterxml.jackson.databind.node.BooleanNode r7 = r11.booleanNode(r0)
            goto L_0x001f
        L_0x0073:
            r0 = 0
            com.fasterxml.jackson.databind.node.BooleanNode r7 = r11.booleanNode(r0)
            goto L_0x001f
        L_0x0079:
            com.fasterxml.jackson.databind.node.NullNode r7 = r11.nullNode()
            goto L_0x001f
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.deser.std.BaseNodeDeserializer.deserializeObject(com.fasterxml.jackson.core.JsonParser, com.fasterxml.jackson.databind.DeserializationContext, com.fasterxml.jackson.databind.node.JsonNodeFactory):com.fasterxml.jackson.databind.node.ObjectNode");
    }

    /* access modifiers changed from: protected */
    public final ArrayNode deserializeArray(JsonParser jsonParser, DeserializationContext deserializationContext, JsonNodeFactory jsonNodeFactory) throws IOException {
        ArrayNode arrayNode = jsonNodeFactory.arrayNode();
        while (true) {
            JsonToken nextToken = jsonParser.nextToken();
            if (nextToken != null) {
                switch (nextToken.mo9113id()) {
                    case 1:
                        arrayNode.add((JsonNode) deserializeObject(jsonParser, deserializationContext, jsonNodeFactory));
                        break;
                    case 3:
                        arrayNode.add((JsonNode) deserializeArray(jsonParser, deserializationContext, jsonNodeFactory));
                        break;
                    case 4:
                        return arrayNode;
                    case 6:
                        arrayNode.add((JsonNode) jsonNodeFactory.textNode(jsonParser.getText()));
                        break;
                    case 7:
                        arrayNode.add(_fromInt(jsonParser, deserializationContext, jsonNodeFactory));
                        break;
                    case 9:
                        arrayNode.add((JsonNode) jsonNodeFactory.booleanNode(true));
                        break;
                    case 10:
                        arrayNode.add((JsonNode) jsonNodeFactory.booleanNode(false));
                        break;
                    case 11:
                        arrayNode.add((JsonNode) jsonNodeFactory.nullNode());
                        break;
                    case 12:
                        arrayNode.add(_fromEmbedded(jsonParser, deserializationContext, jsonNodeFactory));
                        break;
                    default:
                        arrayNode.add(deserializeAny(jsonParser, deserializationContext, jsonNodeFactory));
                        break;
                }
            } else {
                throw deserializationContext.mappingException("Unexpected end-of-input when binding data into ArrayNode");
            }
        }
    }

    /* access modifiers changed from: protected */
    public final JsonNode deserializeAny(JsonParser jsonParser, DeserializationContext deserializationContext, JsonNodeFactory jsonNodeFactory) throws IOException {
        switch (jsonParser.getCurrentTokenId()) {
            case 1:
            case 2:
            case 5:
                return deserializeObject(jsonParser, deserializationContext, jsonNodeFactory);
            case 3:
                return deserializeArray(jsonParser, deserializationContext, jsonNodeFactory);
            case 6:
                return jsonNodeFactory.textNode(jsonParser.getText());
            case 7:
                return _fromInt(jsonParser, deserializationContext, jsonNodeFactory);
            case 8:
                return _fromFloat(jsonParser, deserializationContext, jsonNodeFactory);
            case 9:
                return jsonNodeFactory.booleanNode(true);
            case 10:
                return jsonNodeFactory.booleanNode(false);
            case 11:
                return jsonNodeFactory.nullNode();
            case 12:
                return _fromEmbedded(jsonParser, deserializationContext, jsonNodeFactory);
            default:
                throw deserializationContext.mappingException(handledType());
        }
    }

    /* access modifiers changed from: protected */
    public final JsonNode _fromInt(JsonParser jsonParser, DeserializationContext deserializationContext, JsonNodeFactory jsonNodeFactory) throws IOException {
        NumberType numberType;
        int deserializationFeatures = deserializationContext.getDeserializationFeatures();
        if ((F_MASK_INT_COERCIONS & deserializationFeatures) == 0) {
            numberType = jsonParser.getNumberType();
        } else if (DeserializationFeature.USE_BIG_INTEGER_FOR_INTS.enabledIn(deserializationFeatures)) {
            numberType = NumberType.BIG_INTEGER;
        } else if (DeserializationFeature.USE_LONG_FOR_INTS.enabledIn(deserializationFeatures)) {
            numberType = NumberType.LONG;
        } else {
            numberType = jsonParser.getNumberType();
        }
        if (numberType == NumberType.INT) {
            return jsonNodeFactory.numberNode(jsonParser.getIntValue());
        }
        if (numberType == NumberType.LONG) {
            return jsonNodeFactory.numberNode(jsonParser.getLongValue());
        }
        return jsonNodeFactory.numberNode(jsonParser.getBigIntegerValue());
    }

    /* access modifiers changed from: protected */
    public final JsonNode _fromFloat(JsonParser jsonParser, DeserializationContext deserializationContext, JsonNodeFactory jsonNodeFactory) throws IOException {
        if (jsonParser.getNumberType() == NumberType.BIG_DECIMAL || deserializationContext.isEnabled(DeserializationFeature.USE_BIG_DECIMAL_FOR_FLOATS)) {
            return jsonNodeFactory.numberNode(jsonParser.getDecimalValue());
        }
        return jsonNodeFactory.numberNode(jsonParser.getDoubleValue());
    }

    /* access modifiers changed from: protected */
    public final JsonNode _fromEmbedded(JsonParser jsonParser, DeserializationContext deserializationContext, JsonNodeFactory jsonNodeFactory) throws IOException {
        Object embeddedObject = jsonParser.getEmbeddedObject();
        if (embeddedObject == null) {
            return jsonNodeFactory.nullNode();
        }
        if (embeddedObject.getClass() == byte[].class) {
            return jsonNodeFactory.binaryNode((byte[]) embeddedObject);
        }
        if (embeddedObject instanceof RawValue) {
            return jsonNodeFactory.rawValueNode((RawValue) embeddedObject);
        }
        if (embeddedObject instanceof JsonNode) {
            return (JsonNode) embeddedObject;
        }
        return jsonNodeFactory.pojoNode(embeddedObject);
    }
}
