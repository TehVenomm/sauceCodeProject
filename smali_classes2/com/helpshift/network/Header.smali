.class public Lcom/helpshift/network/Header;
.super Ljava/lang/Object;
.source "Header.java"


# instance fields
.field public final name:Ljava/lang/String;

.field public final value:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 8
    iput-object p1, p0, Lcom/helpshift/network/Header;->name:Ljava/lang/String;

    .line 9
    iput-object p2, p0, Lcom/helpshift/network/Header;->value:Ljava/lang/String;

    return-void
.end method
