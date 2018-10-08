package im.getsocial.p015a.p016a.p017a;

import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

/* renamed from: im.getsocial.a.a.a.pdwpUtZXDT */
public class pdwpUtZXDT extends Exception {
    /* renamed from: a */
    private int f967a;
    /* renamed from: b */
    private Object f968b;
    /* renamed from: c */
    private int f969c;

    public pdwpUtZXDT(int i) {
        this(-1, 2, null);
    }

    public pdwpUtZXDT(int i, int i2, Object obj) {
        this.f969c = i;
        this.f967a = i2;
        this.f968b = obj;
    }

    public pdwpUtZXDT(int i, Object obj) {
        this(-1, 2, obj);
    }

    public String getMessage() {
        StringBuffer stringBuffer = new StringBuffer();
        switch (this.f967a) {
            case 0:
                stringBuffer.append("Unexpected character (").append(this.f968b).append(") at position ").append(this.f969c).append(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
                break;
            case 1:
                stringBuffer.append("Unexpected token ").append(this.f968b).append(" at position ").append(this.f969c).append(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
                break;
            case 2:
                stringBuffer.append("Unexpected exception at position ").append(this.f969c).append(": ").append(this.f968b);
                break;
            default:
                stringBuffer.append("Unkown error at position ").append(this.f969c).append(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
                break;
        }
        return stringBuffer.toString();
    }
}
