package im.getsocial.p015a.p016a.p017a;

import im.getsocial.p015a.p016a.pdwpUtZXDT;
import im.getsocial.p015a.p016a.upgqDBbsrL;
import java.io.IOException;
import java.io.Reader;
import java.io.StringReader;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

/* renamed from: im.getsocial.a.a.a.cjrhisSQCL */
public class cjrhisSQCL {
    /* renamed from: a */
    private LinkedList f963a;
    /* renamed from: b */
    private XdbacJlTDQ f964b = new XdbacJlTDQ(null);
    /* renamed from: c */
    private zoToeBNOjF f965c = null;
    /* renamed from: d */
    private int f966d = 0;

    /* renamed from: a */
    private static int m717a(LinkedList linkedList) {
        return linkedList.size() == 0 ? -1 : ((Integer) linkedList.getFirst()).intValue();
    }

    /* renamed from: a */
    private static Map m718a(jjbQypPegg jjbqyppegg) {
        if (jjbqyppegg == null) {
            return new pdwpUtZXDT();
        }
        Map a = jjbqyppegg.m722a();
        return a == null ? new pdwpUtZXDT() : a;
    }

    /* renamed from: b */
    private static List m719b(jjbQypPegg jjbqyppegg) {
        if (jjbqyppegg == null) {
            return new upgqDBbsrL();
        }
        List b = jjbqyppegg.m723b();
        return b == null ? new upgqDBbsrL() : b;
    }

    /* renamed from: a */
    public final Object m720a(Reader reader, jjbQypPegg jjbqyppegg) {
        this.f964b.m715a(reader);
        this.f965c = null;
        this.f966d = 0;
        this.f963a = null;
        LinkedList linkedList = new LinkedList();
        LinkedList linkedList2 = new LinkedList();
        do {
            this.f965c = this.f964b.m716b();
            if (this.f965c == null) {
                this.f965c = new zoToeBNOjF(-1, null);
            }
            Map a;
            switch (this.f966d) {
                case -1:
                    throw new pdwpUtZXDT(this.f964b.m714a(), 1, this.f965c);
                case 0:
                    try {
                        switch (this.f965c.f970a) {
                            case 0:
                                this.f966d = 1;
                                linkedList.addFirst(new Integer(this.f966d));
                                linkedList2.addFirst(this.f965c.f971b);
                                break;
                            case 1:
                                this.f966d = 2;
                                linkedList.addFirst(new Integer(this.f966d));
                                linkedList2.addFirst(cjrhisSQCL.m718a(jjbqyppegg));
                                break;
                            case 3:
                                this.f966d = 3;
                                linkedList.addFirst(new Integer(this.f966d));
                                linkedList2.addFirst(cjrhisSQCL.m719b(jjbqyppegg));
                                break;
                            default:
                                this.f966d = -1;
                                break;
                        }
                    } catch (IOException e) {
                        throw e;
                    }
                case 1:
                    if (this.f965c.f970a == -1) {
                        return linkedList2.removeFirst();
                    }
                    throw new pdwpUtZXDT(this.f964b.m714a(), 1, this.f965c);
                case 2:
                    switch (this.f965c.f970a) {
                        case 0:
                            if (!(this.f965c.f971b instanceof String)) {
                                this.f966d = -1;
                                break;
                            }
                            linkedList2.addFirst((String) this.f965c.f971b);
                            this.f966d = 4;
                            linkedList.addFirst(new Integer(this.f966d));
                            break;
                        case 2:
                            if (linkedList2.size() <= 1) {
                                this.f966d = 1;
                                break;
                            }
                            linkedList.removeFirst();
                            linkedList2.removeFirst();
                            this.f966d = cjrhisSQCL.m717a(linkedList);
                            break;
                        case 5:
                            break;
                        default:
                            this.f966d = -1;
                            break;
                    }
                case 3:
                    List list;
                    switch (this.f965c.f970a) {
                        case 0:
                            ((List) linkedList2.getFirst()).add(this.f965c.f971b);
                            break;
                        case 1:
                            list = (List) linkedList2.getFirst();
                            a = cjrhisSQCL.m718a(jjbqyppegg);
                            list.add(a);
                            this.f966d = 2;
                            linkedList.addFirst(new Integer(this.f966d));
                            linkedList2.addFirst(a);
                            break;
                        case 3:
                            list = (List) linkedList2.getFirst();
                            List b = cjrhisSQCL.m719b(jjbqyppegg);
                            list.add(b);
                            this.f966d = 3;
                            linkedList.addFirst(new Integer(this.f966d));
                            linkedList2.addFirst(b);
                            break;
                        case 4:
                            if (linkedList2.size() <= 1) {
                                this.f966d = 1;
                                break;
                            }
                            linkedList.removeFirst();
                            linkedList2.removeFirst();
                            this.f966d = cjrhisSQCL.m717a(linkedList);
                            break;
                        case 5:
                            break;
                        default:
                            this.f966d = -1;
                            break;
                    }
                case 4:
                    String str;
                    switch (this.f965c.f970a) {
                        case 0:
                            linkedList.removeFirst();
                            ((Map) linkedList2.getFirst()).put((String) linkedList2.removeFirst(), this.f965c.f971b);
                            this.f966d = cjrhisSQCL.m717a(linkedList);
                            break;
                        case 1:
                            linkedList.removeFirst();
                            str = (String) linkedList2.removeFirst();
                            a = (Map) linkedList2.getFirst();
                            Map a2 = cjrhisSQCL.m718a(jjbqyppegg);
                            a.put(str, a2);
                            this.f966d = 2;
                            linkedList.addFirst(new Integer(this.f966d));
                            linkedList2.addFirst(a2);
                            break;
                        case 3:
                            linkedList.removeFirst();
                            str = (String) linkedList2.removeFirst();
                            a = (Map) linkedList2.getFirst();
                            List b2 = cjrhisSQCL.m719b(jjbqyppegg);
                            a.put(str, b2);
                            this.f966d = 3;
                            linkedList.addFirst(new Integer(this.f966d));
                            linkedList2.addFirst(b2);
                            break;
                        case 6:
                            break;
                        default:
                            this.f966d = -1;
                            break;
                    }
            }
            if (this.f966d == -1) {
                throw new pdwpUtZXDT(this.f964b.m714a(), 1, this.f965c);
            }
        } while (this.f965c.f970a != -1);
        throw new pdwpUtZXDT(this.f964b.m714a(), 1, this.f965c);
    }

    /* renamed from: a */
    public final Object m721a(String str, jjbQypPegg jjbqyppegg) {
        try {
            return m720a(new StringReader(str), null);
        } catch (IOException e) {
            throw new pdwpUtZXDT(-1, 2, e);
        }
    }
}
