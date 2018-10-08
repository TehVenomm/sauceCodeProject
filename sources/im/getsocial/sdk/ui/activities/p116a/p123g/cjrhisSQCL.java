package im.getsocial.sdk.ui.activities.p116a.p123g;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.FrameLayout;
import android.widget.ListAdapter;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.views.MaxHeightListView;
import java.util.List;

@SuppressLint({"ViewConstructor"})
/* renamed from: im.getsocial.sdk.ui.activities.a.g.cjrhisSQCL */
public class cjrhisSQCL extends FrameLayout {
    /* renamed from: a */
    private MaxHeightListView f2746a = ((MaxHeightListView) cjrhisSQCL.inflate(getContext(), C1067R.layout.mentions_view, this).findViewById(C1067R.id.list_view_suggestions));

    public cjrhisSQCL(Context context) {
        super(context);
    }

    /* renamed from: a */
    public final void m3068a(int i) {
        this.f2746a.m3520a(i);
    }

    /* renamed from: a */
    public final <T> void m3069a(Class<? extends jjbQypPegg<T>> cls, final pdwpUtZXDT<T> pdwputzxdt, List<T> list) {
        jjbQypPegg jjbqyppegg;
        if (m3070a((Class) cls)) {
            jjbqyppegg = (jjbQypPegg) cls.cast(this.f2746a.getAdapter());
        } else {
            jjbqyppegg = pdwputzxdt.mo4660a();
            this.f2746a.setAdapter(jjbqyppegg);
        }
        this.f2746a.setOnItemClickListener(new OnItemClickListener(this) {
            /* renamed from: c */
            final /* synthetic */ cjrhisSQCL f2745c;

            public void onItemClick(AdapterView<?> adapterView, View view, int i, long j) {
                pdwputzxdt.mo4661a(jjbqyppegg.getItem(i));
            }
        });
        jjbqyppegg.clear();
        jjbqyppegg.addAll(list);
        jjbqyppegg.notifyDataSetChanged();
    }

    /* renamed from: a */
    public final <T> boolean m3070a(Class<T> cls) {
        ListAdapter adapter = this.f2746a.getAdapter();
        return adapter != null && cls.isInstance(adapter);
    }
}
