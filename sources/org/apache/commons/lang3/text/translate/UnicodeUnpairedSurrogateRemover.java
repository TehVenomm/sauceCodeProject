package org.apache.commons.lang3.text.translate;

import com.fasterxml.jackson.core.base.GeneratorBase;
import java.io.IOException;
import java.io.Writer;

public class UnicodeUnpairedSurrogateRemover extends CodePointTranslator {
    public boolean translate(int i, Writer writer) throws IOException {
        if (i < GeneratorBase.SURR1_FIRST || i > GeneratorBase.SURR2_LAST) {
            return false;
        }
        return true;
    }
}
