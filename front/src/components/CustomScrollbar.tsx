import { useState, useEffect, useRef } from 'react';
import { styled } from '@mui/material/styles';

const Track = styled('div')({
  position: 'absolute',
  top: 0,
  right: 0,
  bottom: 0,            
  width: 8,
  background: 'transparent',
  zIndex: 10,
});

interface ThumbProps {
  thumbHeight: number;
  thumbTop: number;
}

// const Thumb = styled('div')(({ theme, thumbHeight, thumbTop }) => ({
const Thumb = styled('div', {
  shouldForwardProp: (prop) => prop !== 'thumbHeight' && prop !== 'thumbTop',
})<ThumbProps>(({ theme, thumbHeight, thumbTop }) => ({
  position: 'absolute',
  right: 0,
  width: 8,
  height: thumbHeight,
  top: thumbTop,
  background: theme.palette.primary.main,
  borderRadius: 4,
  cursor: 'pointer',
}));

interface CustomScrollbarProps {
  containerRef: React.RefObject<HTMLElement | null>;
  rows: any[];
}

export function CustomScrollbar({ containerRef, rows }: CustomScrollbarProps) {
  const [thumbHeight, setThumbHeight] = useState<number>(0);
  const [thumbTop, setThumbTop] = useState<number>(0);
  const [isScrollable, setIsScrollable] = useState<boolean>(false);

  const dragging = useRef<boolean>(false);
  const dragStartY = useRef<number>(0);
  const startScrollTop = useRef<number>(0);

  useEffect(() => {
    const el = containerRef.current;
    if (!el) return;

    el.scrollTop = 0;

    const calculate = () => {
      const viewH = el.clientHeight;
      const scrollH = el.scrollHeight;
      setIsScrollable(scrollH > viewH);
      const h = Math.max((viewH / scrollH) * viewH, 30);
      setThumbHeight(h);
      setThumbTop((el.scrollTop / scrollH) * viewH);
    };

    calculate();
    el.addEventListener('scroll', calculate);
    window.addEventListener('resize', calculate);

    return () => {
      el.removeEventListener('scroll', calculate);
      window.removeEventListener('resize', calculate);
    };
  }, [containerRef, rows]);

  const onMouseDown = (e: React.MouseEvent<HTMLDivElement>) => {
    dragging.current = true;
    dragStartY.current = e.clientY;
    if (containerRef.current) {
      startScrollTop.current = containerRef.current.scrollTop;
    }
    document.addEventListener('mousemove', onMouseMove);
    document.addEventListener('mouseup', onMouseUp);
    e.preventDefault();
  };

  const onMouseMove = (e: MouseEvent) => {
  if (!dragging.current || !containerRef.current) return;
    
    const el = containerRef.current;
    const deltaY = e.clientY - dragStartY.current;
    const scrollable = el.scrollHeight - el.clientHeight;
    const trackSpace = el.clientHeight - thumbHeight;
    
    el.scrollTop = startScrollTop.current + (deltaY / trackSpace) * scrollable;
  };

  const onMouseUp = () => {
    dragging.current = false;
    document.removeEventListener('mousemove', onMouseMove);
    document.removeEventListener('mouseup', onMouseUp);
  };

  if (!isScrollable) return null;

  return (
    <Track>
      <Thumb
        thumbHeight={thumbHeight}
        thumbTop={thumbTop}
        onMouseDown={onMouseDown}
      />
    </Track>
  );
}