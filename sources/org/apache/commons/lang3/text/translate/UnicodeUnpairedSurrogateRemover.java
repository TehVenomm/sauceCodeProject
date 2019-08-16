package org.apache.commons.lang3.text.translate;

import java.io.IOException;
import java.io.Writer;

public class UnicodeUnpairedSurrogateRemover extends CodePointTranslator {
    public boolean translate(int i, Writer writer) throws IOException {
        if (i < 55296 || i > 57343) {
            return false;
        }
        return true;
    }
}
