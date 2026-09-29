.class public Ljp/colopl/libs/CSpan;
.super Landroid/text/style/ClickableSpan;
.source "CSpan.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Ljp/colopl/libs/CSpan$OnClickListener;
    }
.end annotation


# static fields
.field public static final TYPE_WIFI_SETTING:I = 0x1


# instance fields
.field private mListener:Ljp/colopl/libs/CSpan$OnClickListener;

.field private mType:I


# direct methods
.method public constructor <init>(I)V
    .locals 0

    .line 18
    invoke-direct {p0}, Landroid/text/style/ClickableSpan;-><init>()V

    .line 19
    iput p1, p0, Ljp/colopl/libs/CSpan;->mType:I

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 28
    iget-object v0, p0, Ljp/colopl/libs/CSpan;->mListener:Ljp/colopl/libs/CSpan$OnClickListener;

    if-eqz v0, :cond_0

    .line 29
    iget-object v0, p0, Ljp/colopl/libs/CSpan;->mListener:Ljp/colopl/libs/CSpan$OnClickListener;

    iget v1, p0, Ljp/colopl/libs/CSpan;->mType:I

    invoke-interface {v0, p1, v1}, Ljp/colopl/libs/CSpan$OnClickListener;->onClick(Landroid/view/View;I)V

    :cond_0
    return-void
.end method

.method public setOnClickListener(Ljp/colopl/libs/CSpan$OnClickListener;)V
    .locals 0

    .line 23
    iput-object p1, p0, Ljp/colopl/libs/CSpan;->mListener:Ljp/colopl/libs/CSpan$OnClickListener;

    return-void
.end method
