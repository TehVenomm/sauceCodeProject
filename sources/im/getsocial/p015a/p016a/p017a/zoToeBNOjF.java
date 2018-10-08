package im.getsocial.p015a.p016a.p017a;

/* renamed from: im.getsocial.a.a.a.zoToeBNOjF */
public class zoToeBNOjF {
    /* renamed from: a */
    public int f970a = 0;
    /* renamed from: b */
    public Object f971b = null;

    public zoToeBNOjF(int i, Object obj) {
        this.f970a = i;
        this.f971b = obj;
    }

    public String toString() {
        StringBuffer stringBuffer = new StringBuffer();
        switch (this.f970a) {
            case -1:
                stringBuffer.append("END OF FILE");
                break;
            case 0:
                stringBuffer.append("VALUE(").append(this.f971b).append(")");
                break;
            case 1:
                stringBuffer.append("LEFT BRACE({)");
                break;
            case 2:
                stringBuffer.append("RIGHT BRACE(})");
                break;
            case 3:
                stringBuffer.append("LEFT SQUARE([)");
                break;
            case 4:
                stringBuffer.append("RIGHT SQUARE(])");
                break;
            case 5:
                stringBuffer.append("COMMA(,)");
                break;
            case 6:
                stringBuffer.append("COLON(:)");
                break;
        }
        return stringBuffer.toString();
    }
}
