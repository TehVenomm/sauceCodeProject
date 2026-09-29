.class public final enum Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;
.super Ljava/lang/Enum;
.source "MarkupElement.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/model/news/MarkupElement;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "TextStyle"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

.field public static final enum BOLD:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

.field public static final enum ITALIC:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 170
    new-instance v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    const-string v1, "BOLD"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->BOLD:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    .line 171
    new-instance v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    const-string v1, "ITALIC"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->ITALIC:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    const/4 v0, 0x2

    .line 168
    new-array v0, v0, [Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    sget-object v1, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->BOLD:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    aput-object v1, v0, v2

    sget-object v1, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->ITALIC:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    aput-object v1, v0, v3

    sput-object v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->$VALUES:[Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 168
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;
    .locals 1

    .line 168
    const-class v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    return-object p0
.end method

.method public static values()[Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;
    .locals 1

    .line 168
    sget-object v0, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->$VALUES:[Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    invoke-virtual {v0}, [Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    return-object v0
.end method
